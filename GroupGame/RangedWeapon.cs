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
        public RangedWeapon(Projectile ammoType, Rectangle position, Texture2D sprite, int damage, bool circular, bool equipped) : base(position, sprite, circular, equipped)
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
        public override void Attack()
        {
            int projX = position.X + position.Width / 2 - ammoType.Position.Width/ 2 + (int)(Math.Cos(angle) * position.Width / 2);
            int projY = position.Y + position.Height / 2 - ammoType.Position.Height / 2 + (int)(Math.Sin(angle) * position.Height / 2);
            projectiles.Add(new Projectile(angle, new Rectangle(projX, projY, ammoType.Position.Width, ammoType.Position.Height), ammoType.Speed, damage, ammoType.Sprite, ammoType.CircleBox));
        }

        /// <summary>
        /// calls update in each of the projectiles it has spawned
        /// </summary>
        public override void Update(Rectangle position, double angle)
        {
            base.Update(position, angle);
            for(int i = 0; i<projectiles.Count; i++)
            {
                projectiles[i].Update();
                if (projectiles[i].Damage == -1)
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// calls necessary draws for this weapon
        /// </summary>
        /// <param name="sb">spritebatch used to draw</param>
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            DrawProjectiles(sb);
        }

        /// <summary>
        /// draws all projectiles created by this weapon
        /// </summary>
        /// <param name="sb">spritebatch used to draw</param>
        public void DrawProjectiles(SpriteBatch sb)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(sb);
            }
        }


    }
}
