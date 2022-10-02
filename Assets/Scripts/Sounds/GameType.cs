using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helzinko
{
    public class GameType: MonoBehaviour
    {
        public enum AudioTypes
        {
            effects = 0,
            music = 1,
        }

        public enum SoundTypes
        {
            playerShoot = 0,
            playerDie = 1,
            cubeDie = 2,
            cubeHitGround = 3,
            enemyHurt = 4,
            playerJump = 5,
            enemyShoot = 6,
            playerHurt = 7,
            bulletHit = 8,
            lavaMove = 9,
        }
    }

}
