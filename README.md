# NavigationHistory
Simple .NET Standard library to handle history navigation (back, forward)

```csharp
NavigationHistory.Record(TestNavigationItems.HomePage);
NavigationHistory.Record(TestNavigationItems.Page1);
NavigationHistory.Record(TestNavigationItems.Page2);

NavigationHistory.Back();     // TestNavigationItems.Page1
NavigationHistory.Forward();  // TestNavigationItems.Page2
NavigationHistory.Back();     // TestNavigationItems.Page1
NavigationHistory.Back();     // TestNavigationItems.HomePage

NavigationHistory.CurrentItem; // TestNavigationItems.HomePage
```

![Console visualization](http://oi68.tinypic.com/ekp06d.jpg)
