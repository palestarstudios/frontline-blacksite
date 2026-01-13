using System;

namespace Blacksite.InteractiveObjects
{
    // The AbstractEntity is a base class, everything that can be controlled by the
    // physics system or by AI/the player qualifies as an entity and should
    // be extended by this base class. Examples include:
    //
    // - The player
    // - Non-Player Bots
    // - Projectiles that have been launched/thrown
    // - Naturally spawning health / ammo packages
    // - Items that have been placed by the Engineer;
    //      * Barricades
    //      * Offensive sentries
    abstract class AbstractEntity
    {
        private string EntityID;

        public AbstractEntity()
        {
            this.EntityID = Guid.NewGuid().ToString();
        }

        public abstract void Update();
        public abstract void Draw();

        /// <summary>
        /// Returns the Entities base GUID as a string, usually for logging / crash dumps
        /// </summary>
        /// <returns>string EntityID</returns>
        protected string GetEntityGUID()
        {
            return this.EntityID;
        }
    }
}