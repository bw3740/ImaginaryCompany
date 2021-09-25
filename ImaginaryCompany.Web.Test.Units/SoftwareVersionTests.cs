using Xunit;
using ImaginaryCompany.Web.Data.Models;

namespace ImaginaryCompany.Web.Test.Units
{
    public class SoftwareVersionTests
    {
        [Fact]
        public void Simple_Valid_Version_Succeeds()
        {
            var v = new SoftwareVersion("1.2.3");
            Assert.Equal(1, v.Major);
            Assert.Equal(2, v.Minor);
            Assert.Equal(3, v.Patch);
        }

        [Fact]
        public void Extra_Build_Info_Version_Succeeds()
        {
            var v = new SoftwareVersion("1.2.3.888");
            Assert.Equal(1, v.Major);
            Assert.Equal(2, v.Minor);
            Assert.Equal(3, v.Patch);
        }

        [Fact]
        public void Complex_Valid_Version_Succeeds()
        {
            var v = new SoftwareVersion("001.4321.99999999");
            Assert.Equal(1, v.Major);
            Assert.Equal(4321, v.Minor);
            Assert.Equal(99999999, v.Patch);
        }

        [Fact]
        public void Major_Only_Succeeds()
        {
            var v = new SoftwareVersion("123");
            Assert.Equal(123, v.Major);
            Assert.Equal(0, v.Minor);
            Assert.Equal(0, v.Patch);
        }

        [Fact]
        public void Major_And_Minor_Only_Succeeds()
        {
            var v = new SoftwareVersion("1.2");
            Assert.Equal(1, v.Major);
            Assert.Equal(2, v.Minor);
            Assert.Equal(0, v.Patch);
        }

        [Fact]
        public void Empty_Returns_Zeros()
        {
            var v = new SoftwareVersion("");
            Assert.Equal(0, v.Major);
            Assert.Equal(0, v.Minor);
            Assert.Equal(0, v.Patch);
        }

        [Fact]
        public void NonNumeric1_Returns_Zeros()
        {
            var v = new SoftwareVersion("abc");
            Assert.Equal(0, v.Major);
            Assert.Equal(0, v.Minor);
            Assert.Equal(0, v.Patch);
        }

        [Fact]
        public void NonNumeric2_Returns_Zeros()
        {
            var v = new SoftwareVersion("*.*");
            Assert.Equal(0, v.Major);
            Assert.Equal(0, v.Minor);
            Assert.Equal(0, v.Patch);
        }

        [Fact]
        public void Major_Greater_Succeeds()
        {
            var v1 = new SoftwareVersion("2.1");
            var v2 = new SoftwareVersion("1.1.0");
            Assert.True(v1.IsGreater(v2));
        }

        [Fact]
        public void Major_Lesser_Succeeds()
        {
            var v1 = new SoftwareVersion("1.1");
            var v2 = new SoftwareVersion("2.1.0");
            Assert.False(v1.IsGreater(v2));
        }

        [Fact]
        public void Minor_Greater_Succeeds()
        {
            var v1 = new SoftwareVersion("2.2");
            var v2 = new SoftwareVersion("2.1");
            Assert.True(v1.IsGreater(v2));
        }

        [Fact]
        public void Minor_Lesser_Succeeds()
        {
            var v1 = new SoftwareVersion("2.1");
            var v2 = new SoftwareVersion("2.2.0");
            Assert.False(v1.IsGreater(v2));
        }

        [Fact]
        public void Patch_Greater_Succeeds()
        {
            var v1 = new SoftwareVersion("2.2.8");
            var v2 = new SoftwareVersion("2.2.4");
            Assert.True(v1.IsGreater(v2));
        }

        [Fact]
        public void Patch_Lesser_Succeeds()
        {
            var v1 = new SoftwareVersion("2.1.8");
            var v2 = new SoftwareVersion("2.1.9");
            Assert.False(v1.IsGreater(v2));
        }

        [Fact]
        public void Simple_Formatted_Succeeds()
        {
            var v = new SoftwareVersion("1");
            Assert.Equal("1.0.0", v.ToFormattedString());
        }

        [Fact]
        public void Complex_Formatted_Succeeds()
        {
            var v = new SoftwareVersion("123.55555.123456789");
            Assert.Equal("123.55555.123456789", v.ToFormattedString());
        }
    }
}
