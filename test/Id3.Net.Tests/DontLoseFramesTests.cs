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

using Xunit;
using Xunit.Abstractions;

namespace Id3.Net.Tests
{
    public sealed class DontLoseFramesTests
    {
        private readonly ITestOutputHelper output;
        const string dirStart = @"C:\Users\mdepouw\source\repos\GitHub\spottedmahn\Id3\test\Data\unknown frames";

        public DontLoseFramesTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void DontLoseFramesTest1()
        {
            //arrange
            var path = $@"{dirStart}\01-02- DNA [Explicit].mp3";
            var mp3 = new Mp3(path, Mp3Permissions.ReadWrite);

            //act 
            //should throw an exception that data will be lost
            mp3.GetTag(Id3TagFamily.Version2X);

            //assert
            Assert.True(false);
        }

        [Fact]
        public void NotReadingWholeId3TagDebug()
        {
            //arrange
            var path = $@"{dirStart}\01-02- DNA [Explicit] - read all of id3 tag.mp3";
            var mp3 = new Mp3(path, Mp3Permissions.ReadWrite);
            var tag = mp3.GetTag(Id3TagFamily.Version2X);

            //act 
            
            //assert
            Assert.True(false);
        }
    }
}
