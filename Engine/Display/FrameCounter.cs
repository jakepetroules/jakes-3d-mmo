namespace MMO3D.Engine
{
    using System;

    /// <summary>
    /// A class to count the number of frames the game is rendering per second.
    /// </summary>
    public sealed class FrameCounter
    {
        /// <summary>
        /// The last second at which we captured the time.
        /// </summary>
        private double lastSecond = DateTime.Now.TimeOfDay.TotalMilliseconds;

        /// <summary>
        /// The counter to count the number of frames between intervals.
        /// </summary>
        private int fpsCount = 0;

        /// <summary>
        /// Gets the current rendering speed of the game in frames per second.
        /// </summary>
        /// <value>See summary.</value>
        public int Fps
        {
            get;
            private set;
        }

        /// <summary>
        /// Updates the counter. This should be called at the end of <c>Game.Draw(GameTime)</c>, before <c>base.Draw(GameTime)</c>.
        /// </summary>
        public void Update()
        {
            if (DateTime.Now.TimeOfDay.TotalMilliseconds >= (this.lastSecond + 1000))
            {
                this.Fps = this.fpsCount;
                this.fpsCount = 0;
                this.lastSecond = DateTime.Now.TimeOfDay.TotalMilliseconds;
            }

            this.fpsCount++;
        }
    }
}
