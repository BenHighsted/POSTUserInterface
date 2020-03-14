using Microsoft.VisualStudio.TestTools.UnitTesting;
using PointOfSaleTerminalLibrary;

namespace PointOfSaleUnitTests
{
	[TestClass]
	public class BaseTests
	{
		//Methods to test valid inputs
		[TestMethod]
		public void TestBaseCase1()
		{
			App runCode = new App();
			Assert.AreEqual(13.25M, runCode.ScanItems("A,B,C,D,A,B,A"));
		}
		[TestMethod]
		public void TestBaseCase2()
		{
			App runCode = new App();
			Assert.AreEqual(6.00M, runCode.ScanItems("C,C,C,C,C,C,C"));
		}
		[TestMethod]
		public void TestBaseCase3()
		{
			App runCode = new App();
			Assert.AreEqual(7.25M, runCode.ScanItems("A,B,C,D"));
		}
	}

	[TestClass]
	public class BadInputs 
	{ 
		//Methods to test inputs with all/some invalid inputs
		[TestMethod]
		public void TestNoInput()
		{
			App runCode = new App();
			Assert.AreEqual(0M, runCode.ScanItems(""));
		}
		[TestMethod]
		public void TestInvalidInput()
		{
			App runCode = new App();
			Assert.AreEqual(0M, runCode.ScanItems("E,F,G"));
		}
		[TestMethod]
		public void TestInvalidAndValidInput()
		{
			App runCode = new App();
			Assert.AreEqual(19.00M, runCode.ScanItems("E,A,B,F,G,A,C,D,D,D,B,B,E,X,Z,V,A"));
		}
		[TestMethod]
		public void TestInvalidAndValidInput2()
		{
			App runCode = new App();
			Assert.AreEqual(7.25M, runCode.ScanItems("E,F,A,B,Z,X,D,C,G,G"));
		}
	}

	[TestClass]
	public class ManyInputs
	{
		//Methods that test large inputs
		[TestMethod]
		public void TestMutipleDealsInputs()
		{
			App runCode = new App();
			Assert.AreEqual(9.00M, runCode.ScanItems("A,A,A,A,A,A,A,A,A"));
		}
		[TestMethod]
		public void TestMutipleDealsInputs2()
		{
			App runCode = new App();
			Assert.AreEqual(15.00M, runCode.ScanItems("C,C,C,C,C,C,C,C,C,C,C,C,C,C,C,C,C,C"));
		}
		[TestMethod]
		public void TestMutipleDealsInputs3()
		{
			App runCode = new App();
			Assert.AreEqual(13.00M, runCode.ScanItems("C,C,C,C,C,C,A,A,A,B,D"));
		}
		[TestMethod]
		public void HugeAmount()
		{
			App runCode = new App();
			Assert.AreEqual(69.25M, runCode.ScanItems("A,B,C,D,A,B,C,D,A,B,C,D,A,B,C,D,A,B,C,D,A,B,C,D,A,B,C,D,A,B,C,D,A,B,C,D,A,B,C,D"));
		}
	}
}
