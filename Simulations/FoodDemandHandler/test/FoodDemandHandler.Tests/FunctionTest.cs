using Xunit;

namespace FoodDemandHandler.Tests;

public class FunctionTest
{
	[Fact]
	public void Stub()
	{
		var sut = new Sut() as Interf2;
		var type = sut.GetType();
		Assert.True(true);
	}

	public interface Interf1 : Interf2
	{

	}
	public interface Interf2
	{

	}

	public class Sut : Interf1
	{

	}
}