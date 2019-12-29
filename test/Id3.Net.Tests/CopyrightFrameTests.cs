#region --- License & Copyright Notice ---
/*
Copyright (c) 2005-2018 Jeevan James
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
#endregion

using Id3.Frames;
using Xunit;
using Xunit.Abstractions;

namespace Id3.Net.Tests
{
    public sealed class CopyrightFrameTests
    {
        private readonly ITestOutputHelper output;
        const string dirStart = @"C:\Users\mdepouw\source\repos\GitHub\spottedmahn\Id3\test\Data\copyright";

        public CopyrightFrameTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        //[Fact]
        public void TRCK_BugTest()
        {
            //arrange
            var path = $@"{dirStart}\01-02- DNA [Explicit] - Copy.mp3";
            var mp3 = new Mp3(path, Mp3Permissions.ReadWrite);
            var tag = mp3.GetTag(Id3TagFamily.Version2X);
            //works after changing regex
            //removing start and end things
            //data that doesn't currently work:
            //var data = new byte[] { 0, 50, 47, 49, 53, 0 };
            //was
            //Regex TrackPattern = new Regex(@"^(\d+)(?:/(\d+))?$");
            //now
            //Regex TrackPattern = new Regex(@"(\d+)(?:/(\d+))?");
            Assert.Equal(2, tag.Track.Value);
            Assert.Equal(15, tag.Track.TrackCount);

            //act 
            //should throw an exception that data will be lost
            var newMp3 = new Mp3(path + " - trck test.mp3", Mp3Permissions.ReadWrite);
            newMp3.WriteTag(tag);
            newMp3.Dispose();

            //assert
            var newMp3Assert = new Mp3(path + " - trck test.mp3", Mp3Permissions.Read);
            var tagAssert = newMp3Assert.GetTag(Id3TagFamily.Version2X);
            Assert.Equal(tag.Track.TrackCount, tagAssert.Track.TrackCount);
            Assert.Equal(tag.Track.Value, tagAssert.Track.Value);
        }

        [Fact]
        public void CopyrightFrameTest1()
        {
            //arrange
            var copyright = new CopyrightFrame();

            //act && assert
            copyright.EncodingType = Id3TextEncoding.Unicode;
            Assert.Throws<InvalidCopyrightFrameException>(() => copyright.TextValue = "(C) 2017 Aftermath/Interscope (Top Dawg Entertainment)")            ;
        }
    }
}
