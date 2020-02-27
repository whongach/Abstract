using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupGame
{
    class RangedWeapon : Weapon
    {
        //fields
        private Projectile ammoType;
        private List<Projectile> projectiles;

        //properties
        public Projectile AmmoType
        {
            get { return ammoType; }
            set { ammoType = value; }
        }

        public List<Projectile> Projectiles
        {
            get { return projectiles; }
            set { projectiles = value; }
        }

        //constructor
        public RangedWeapon(Projectile ammoType, Rectangle position, Texture2D sprite, int damage) : base(position, sprite)
        {
            this.damage = damage;
            ammoType.Damage = damage;
            this.ammoType = ammoType;
            projectiles = new List<Projectile>();
        }

        //methods

        /// <summary>
        /// This method creates a projectile at the position of the player going at the correct angle
        /// </summary>
        /// <param name="position"></param>
        /// <param name="angle"></param>
        public override void Attack(Vector2 position, double angle)
        {
            ammoType.Angle = angle;
            ammoType.Position = new Rectangle((int)position.X, (int)position.Y, ammoType.Position.Width, ammoType.Position.Height);
            projectiles.Add(ammoType);
        }

        /// <summary>
        /// calls update in each of the projectiles it has spawned
        /// </summary>
        public override void Update()
        {
            for(int i = 0; i<projectiles.Count; i++)
            {
                projectiles[i].Update();
            }
        }
    }
}
