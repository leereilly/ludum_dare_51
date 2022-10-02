using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Helzinko
{
    [DefaultExecutionOrder(-100)]
    public class Gameplay : MonoBehaviour
    {
        [SerializeField] private Vector3 playerInitPos;
        [SerializeField] private Vector3 lavaInitPos;
        [SerializeField] private Vector3 blocksInitPos;

        [SerializeField] private Player playerPrefab;
        [SerializeField] private Lava lavaPrefab;
        [SerializeField] private GameObject blocksContainerPrefab;
        [SerializeField] private Transform cursor;
        [SerializeField] private CinemachineVirtualCamera cCamera;
        

        [SerializeField] private HUD hud;
        [SerializeField] private Lava lava;
        public Player player { private set; get; }

        public Grid grid;

        private const float waveTime = 10f;

        public static Gameplay instance;

        List<ILoadable> Loaded = new List<ILoadable>();

        private float timeSinceLastWave = 0f;

        private float playerSurvivedTime = 0f;
        private int playerMetersRecord = 0;

        private bool playerCreated = false;

        public int gameplayLevel = 0;

        private int sessionRecord = 0;

        [SerializeField] private GameObject recordObject;
        [SerializeField] private TextMesh recordText;

        private void Awake()
        {
            instance = this;

            Cursor.visible = false;

            hud.ActiveStartText(true);

            recordObject.SetActive(false);
        }

        private void Start()
        {
            SoundManager.instance.PlayMusic(GameType.SoundTypes.music);
        }

        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                if (!playerCreated)
                {
                    LoadLevel();
                }
            }

            if (timeSinceLastWave >= waveTime)
            {
                lava.MoveUp();
                timeSinceLastWave = 0;

                gameplayLevel++;
            }

            if (playerCreated)
            {
                timeSinceLastWave += Time.deltaTime;
                playerSurvivedTime += Time.deltaTime;

                if (Mathf.RoundToInt(player.transform.position.y) > playerMetersRecord)
                {
                    playerMetersRecord = Mathf.RoundToInt(player.transform.position.y);
                    
                    if(Mathf.RoundToInt(player.transform.position.y) > sessionRecord)
                    {
                        sessionRecord = Mathf.RoundToInt(player.transform.position.y);
                        recordObject.transform.position = new Vector2(recordObject.transform.position.x, sessionRecord);
                        recordText.text = sessionRecord.ToString() + " m";
                    }
                }

            }

            hud.UpdateVoidBar(Mathf.RoundToInt(timeSinceLastWave));
            hud.UpdateTimer(playerSurvivedTime);
            hud.UpdateMeters(playerMetersRecord);
        }

        public void LoadElement(ILoadable loadable)
        {
            loadable.OnLoad();
            Loaded.Add(loadable);
        }
        public void UnloadElement(ILoadable loadable)
        {
            loadable.OnUnload();
            Loaded.Remove(loadable);
        }

        private void LoadLevel()
        {
            recordObject.SetActive(true);

            playerCreated = true;

            player = Instantiate(playerPrefab, playerInitPos, default, null);
            player.SetCursor(cursor);
            player.OnDie.AddListener(PlayerDied);
            cCamera.Follow = player.transform;
            var blocks = Instantiate(blocksContainerPrefab, blocksInitPos, default, null);
            lava.Reset(lavaInitPos);

            hud.ActiveResultText(false);
            hud.ActiveStartText(false);
        }

        private void UnloadLevel()
        {
            foreach(var item in Loaded)
            {
                if(item != null)
                    item.OnUnload();
            }

            Loaded.Clear();
        }

        private void PlayerDied()
        {
            UnloadLevel();

            hud.UpdateResults(playerMetersRecord, playerSurvivedTime);

            player.OnDie.RemoveListener(PlayerDied);
            cursor.gameObject.SetActive(false);
            playerCreated = false;
            timeSinceLastWave = 0;
            playerSurvivedTime = 0;
            playerMetersRecord = 0;

            hud.ActiveResultText(true);
            hud.ActiveStartText(true);

            gameplayLevel = 0;
        }
    }
}
