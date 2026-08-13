using FluentAssertions;
using LOTR_GameRegister.Api.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Globalization;
using System.Resources;

namespace LOTR_GameRegister.Tests;

[TestClass]
public class LocalizerTests
{
    private static readonly ResourceManager English =
        new("LOTR_GameRegister.Api.Resources.Messages", typeof(Localizer).Assembly);

    [TestMethod]
    public void SpanishResources_DefineEveryEnglishKey()
    {
        var spanish = CultureTestHelper.TryCreate("es-ES");
        if (spanish is null)
        {
            Assert.Inconclusive("Culture 'es-ES' is not available in this environment (globalization invariant mode).");
            return;
        }

        var original = CultureInfo.CurrentUICulture;
        try
        {
            CultureInfo.CurrentUICulture = spanish;

            var resourceSet = English.GetResourceSet(CultureInfo.InvariantCulture, true, true)!;

            foreach (DictionaryEntry entry in resourceSet)
            {
                var key = (string)entry.Key;
                var value = Localizer.Get(key);

                value.Should().NotBeNullOrWhiteSpace($"key '{key}' must have a Spanish translation");
                value.Should().NotBe(key, $"key '{key}' must be translated, not fall back to the key");
            }
        }
        finally
        {
            CultureInfo.CurrentUICulture = original;
        }
    }

    [TestMethod]
    public void Get_ReturnsEnglishByDefault()
    {
        var original = CultureInfo.CurrentUICulture;
        try
        {
            CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;

            Localizer.Get("UserRegistered").Should().Be("User registered successfully.");
        }
        finally
        {
            CultureInfo.CurrentUICulture = original;
        }
    }

    [TestMethod]
    public void Get_ReturnsSpanishWhenRequested()
    {
        var spanish = CultureTestHelper.TryCreate("es-ES");
        if (spanish is null)
        {
            Assert.Inconclusive("Culture 'es-ES' is not available in this environment (globalization invariant mode).");
            return;
        }

        var original = CultureInfo.CurrentUICulture;
        try
        {
            CultureInfo.CurrentUICulture = spanish;

            Localizer.Get("UserRegistered").Should().Be("Usuario registrado correctamente.");
        }
        finally
        {
            CultureInfo.CurrentUICulture = original;
        }
    }

    [TestMethod]
    public void Get_FormatsArguments()
    {
        var original = CultureInfo.CurrentUICulture;
        try
        {
            CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;

            Localizer.Get("GameNotFound", 42).Should().Be("Game with ID 42 not found.");
        }
        finally
        {
            CultureInfo.CurrentUICulture = original;
        }
    }

    [TestMethod]
    public void Get_WithUnknownKey_ReturnsTheKeyItself()
    {
        Localizer.Get("__missing_key__").Should().Be("__missing_key__");
    }
}
