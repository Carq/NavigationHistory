# NavigationHistory
Simple .NET Standard library to handle history navigation (back, forward)

```csharp
NavigationHistory navigationHistory = new NavigationHistory<TestNavigationItem>();

navigationHistory.Record(TestNavigationItems.HomePage);
navigationHistory.Record(TestNavigationItems.Page1);
navigationHistory.Record(TestNavigationItems.Page2);

navigationHistory.Back();     // TestNavigationItems.Page1
navigationHistory.Forward();  // TestNavigationItems.Page2
navigationHistory.Back();     // TestNavigationItems.Page1
navigationHistory.Back();     // TestNavigationItems.HomePage

navigationHistory.CurrentItem; // TestNavigationItems.HomePage
```

![Console visualization](http://oi68.tinypic.com/ekp06d.jpg)
