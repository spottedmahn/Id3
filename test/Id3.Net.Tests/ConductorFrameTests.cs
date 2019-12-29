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
    public sealed class ConductorFrameTests
    {
        private readonly ITestOutputHelper output;
        const string dirStart = @"C:\Users\mdepouw\source\repos\GitHub\spottedmahn\Id3\test\Data\conductor";

        public ConductorFrameTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TPE3_BugTest()
        {
            //arrange
            var path = $@"{dirStart}\01-02- DNA [Explicit].mp3";
            //path = $@"{dirStart}\01-02- DNA [Explicit] - foobar2000 test.mp3";
            var mp3 = new Mp3(path, Mp3Permissions.ReadWrite);
            var tag = mp3.GetTag(Id3TagFamily.Version2X);

            //act 
            tag.Conductor.TextValue = "Mike D.'s the conductor";
            tag.Conductor.TextValue = "M";
            var newMp3 = new Mp3(path + " - conductor test.mp3", Mp3Permissions.ReadWrite);
            newMp3.WriteTag(tag);
            newMp3.Dispose();

            //assert
            var newMp3Assert = new Mp3(path + " - conductor test.mp3", Mp3Permissions.Read);
            var tagAssert = newMp3Assert.GetTag(Id3TagFamily.Version2X);
            Assert.Equal("Mike D.'s the conductor", tagAssert.Conductor.Value);
        }

        [Fact]
        public void CopyrightFrameTest1()
        {
            //arrange
            var copyright = new CopyrightFrame();

            //act && assert
            copyright.EncodingType = Id3TextEncoding.Unicode;
            Assert.Throws<InvalidCopyrightFrameException>(() => copyright.TextValue = "(C) 2017 Aftermath/Interscope (Top Dawg Entertainment)");
        }
    }
}
