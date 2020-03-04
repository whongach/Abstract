using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupGame
{
    class MeleeWeapon : Weapon, ICollidable
    {
        //fields
        int rotationDegrees;
        int rotationSpeed;
        bool attacking;
        int attackFrame;
        int totalFrames;

        //properties
        public bool Attacking
        {
            get { return attacking; }
            set { attacking = value; }
        }

        //constructor
        public MeleeWeapon(Rectangle position, Texture2D sprite, bool circular, bool equipped, int rotationDegrees, int rotationSpeed) : base(position, sprite, circular, equipped)
        {
            this.rotationDegrees = rotationDegrees;
            this.rotationSpeed = rotationSpeed;
            totalFrames = rotationDegrees / rotationSpeed;
            this.attacking = false;
        }

        //methods

        //temporary attack method
        public override void Attack()
        {
            throw new NotImplementedException();
        }
    }
}
