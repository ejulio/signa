using Signa.Model;
using System.Collections.Generic;

namespace Signa.Tests.Common.Builders
{
    public class SignSampleBuilder
    {
        private IList<SignFrame> frames;

        public SignSampleBuilder WithFrames(IList<SignFrame> frames)
        {
            this.frames = frames;
            return this;
        }

        public SignSample Build()
        {
            return new SignSample
            {
                Frames = frames
            };
        }
    }
}
