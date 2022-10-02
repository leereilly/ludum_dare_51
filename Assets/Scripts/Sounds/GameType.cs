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
            pickupPowerUp = 1,
            enemyDeath = 2,
            music = 3,
            bullet_hit = 4,
            enemy_spawn = 5,
            enemy_explode = 6,
            enemy_shoot = 7,
            player_roll = 8,
            powerup_spawn = 9,
            powerup_pick = 10,
            player_die = 11,
            chest_ended = 12,
            ui_select = 13,
            unlockable_enter = 14,
            unlockable_pin = 15,
        }
    }

}
