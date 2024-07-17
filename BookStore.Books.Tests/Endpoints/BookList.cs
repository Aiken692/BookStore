﻿using BookStore.Books.BookEndpoints;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using Xunit.Abstractions;

namespace BookStore.Books.Tests.Endpoints;

public class BookList(Fixture fixture, ITestOutputHelper outputHelper) :
  TestClass<Fixture>(fixture, outputHelper)
{
  [Fact]
  public async Task ReturnsThreeBooksAsync()
  {
    var testResult = await Fixture.Client.GETAsync<List, ListBooksResponse>();

    testResult.Response.EnsureSuccessStatusCode();
    testResult.Result.Books.Count.Should().Be(3);
  }
}

