using System;

namespace PointOfSaleTerminalLibrary
{
	public class PointOfSaleTerminal
	{
		public int[] numberOfProducts = new int[4];
		public decimal[] valueOfProducts = new decimal[4];

		public const int aDiscountQuantity = 3;
		public const int cDiscountQuantity = 6;

		public const decimal aDiscountAmount = 0.75M;
		public const decimal cDiscountAmount = 1.00M;

		public decimal savings = 0.0M;

		/// <summary>
		///	This method sets the price of each of the products (i.e. A)
		/// </summary>
		/// <param name="A">The new price of product 'A'</param>
		/// <param name="B">The new price of product 'B'</param>
		/// <param name="C">The new price of product 'C'</param>
		/// <param name="D">The new price of product 'D'</param>
		public void SetPricing(decimal A, decimal B, decimal C, decimal D)
		{
			valueOfProducts[0] = A;
			valueOfProducts[1] = B;
			valueOfProducts[2] = C;
			valueOfProducts[3] = D;
		}

		/// <summary>
		/// This method scans a single product in and adds to the array to indicate what was added
		/// </summary>
		/// <param name="itemScanned">The input to be added</param>
		public void ScanProduct(String itemScanned)
		{
			if (itemScanned.Equals("A"))
				numberOfProducts[0]++;
			else if (itemScanned.Equals("B"))
				numberOfProducts[1]++;
			else if (itemScanned.Equals("C"))
				numberOfProducts[2]++;
			else if (itemScanned.Equals("D"))
				numberOfProducts[3]++;
			else
				Console.WriteLine(itemScanned + " is an invalid input so will not be counted towards your total.");
		}

		/// <summary>
		/// Calculates the total cost (called after all items have been scanned in) including removing savings
		/// </summary>
		/// <returns>Returns the total cost</returns>
		public decimal CalculateTotal()
		{
			decimal total = 0.0M;
			Boolean discounts = true;

			for (int i = 0; i < numberOfProducts.Length; i++)
			{
				total += valueOfProducts[i] * numberOfProducts[i];
			}

			savings = total;

			while (discounts) 
			{
				if (numberOfProducts[0] >= aDiscountQuantity)
				{
					total -= aDiscountAmount;
					numberOfProducts[0] -= aDiscountQuantity;
				}
				else if (numberOfProducts[2] >= cDiscountQuantity)
				{
					total -= cDiscountAmount;
					numberOfProducts[2] -= cDiscountQuantity;
				}
				else
					discounts = false;
			}

			savings -= total;

			return total;
		}

		/// <summary>
		/// A method used for the GUI to return how much has been saved (so far).
		/// </summary>
		/// <returns>Returns the total amount saved</returns>
		public decimal CalculateSavings() 
		{
			return savings;
		}

	}
	public class App
	{
		/// <summary>
		/// This method is used to run all the methods in the library (i.e. for unit tests to call)
		/// </summary>
		/// <param name="input">The string input containing what needs to be scanned in</param>
		/// <returns>Returns the result of calling the CalculateTotal() method</returns>
		public decimal ScanItems(String input)
		{
			String[] inputs = input.Split(',');

			PointOfSaleTerminal terminal = new PointOfSaleTerminal();
			terminal.SetPricing(1.25M, 4.25M, 1.00M, 0.75M);
			foreach (string v in inputs)
			{
				terminal.ScanProduct(v);
			}
			decimal result = terminal.CalculateTotal();

			Console.WriteLine(input + " totals to: $" + result); //Used for testing purposes, not needed for final product.

			return result;
		}
	}
}