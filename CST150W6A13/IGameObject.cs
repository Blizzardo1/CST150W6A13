using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST150W6A13
{
    public interface IGameObject
    {
        /// <summary>
        /// Initializes the Game Object
        /// </summary>
        void Initialize();

        /// <summary>
        /// Draw Loop containing all drawing routines
        /// </summary>
        void Draw(TimeSpan deltaTime);

        /// <summary>
        /// Update loop containing all logic
        /// </summary>
        void Update(TimeSpan deltaTime);
    }
}
