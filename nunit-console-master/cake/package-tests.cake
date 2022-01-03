//////////////////////////////////////////////////////////////////////
// INDIVIDUAL PACKAGE TEST DEFINITIONS
//////////////////////////////////////////////////////////////////////

static ExpectedResult MockAssemblyExpectedResult = new ExpectedResult("Failed")
{
    Total = 37,
    Passed = 23,
    Failed = 5,
    Warnings = 1,
    Inconclusive = 1,
    Skipped = 7
};

static ExpectedResult TwoMockAssembliesExpectedResult = new ExpectedResult("Failed")
{
    Total = 2*37,
    Passed = 2*23,
    Failed = 2*5,
    Warnings = 2*1,
    Inconclusive = 2*1,
    Skipped = 2*7
};

static PackageTest Net35Test = new PackageTest(
    "Run mock-assembly.dll under .NET 3.5",
    "net35/mock-assembly.dll",
    MockAssemblyExpectedResult);

static PackageTest Net35X86Test = new PackageTest(
    "Run mock-assembly-x86.dll under .NET 3.5",
    "net35/mock-assembly-x86.dll",
    MockAssemblyExpectedResult);

static PackageTest Net40Test = new PackageTest(
    "Run mock-assembly.dll under .NET 4.x",
    "net40/mock-assembly.dll",
    MockAssemblyExpectedResult);

static PackageTest Net40X86Test = new PackageTest(
    "Run mock-assembly-x86.dll under .NET 4.x",
    "net40/mock-assembly-x86.dll",
    MockAssemblyExpectedResult);

static PackageTest Net35PlusNet40Test = new PackageTest(
    "Run both copies of mock-assembly together",
    "net35/mock-assembly.dll net40/mock-assembly.dll",
    TwoMockAssembliesExpectedResult);

static PackageTest NetCore31Test = new PackageTest(
    "Run mock-assembly.dll under .NET Core 3.1",
    "netcoreapp3.1/mock-assembly.dll",
    MockAssemblyExpectedResult);

static PackageTest NetCore31X86Test = new PackageTest(
    "Run mock-assembly-x86.dll under .NET Core 3.1",
    "netcoreapp3.1/mock-assembly-x86.dll",
    MockAssemblyExpectedResult);

static PackageTest NetCore21Test = new PackageTest(
        "Run mock-assembly.dll targeting .NET Core 2.1",
        "netcoreapp2.1/mock-assembly.dll",
    MockAssemblyExpectedResult);

static PackageTest NetCore21X86Test = new PackageTest(
    "Run mock-assembly-x86.dll under .NET Core 2.1",
    "netcoreapp2.1/mock-assembly-x86.dll",
    MockAssemblyExpectedResult);

static PackageTest NetCore21PlusNetCore31Test = new PackageTest(
    "Run both copies of mock-assembly together",
    "netcoreapp2.1/mock-assembly.dll netcoreapp3.1/mock-assembly.dll",
    TwoMockAssembliesExpectedResult);

static PackageTest NUnitProjectTest;
NUnitProjectTest = new PackageTest(
    "Run project with both copies of mock-assembly",
    $"../../NetFXTests.nunit --config={Argument("configuration", "Release")}",
    TwoMockAssembliesExpectedResult);

// Representation of a single test to be run against a pre-built package.
public struct PackageTest
{
    public string Description;
    public string Arguments;
    public ExpectedResult ExpectedResult;

    public PackageTest(string description, string arguments, ExpectedResult expectedResult)
    {
        Description = description;
        Arguments = arguments;
        ExpectedResult = expectedResult;
    }
}

