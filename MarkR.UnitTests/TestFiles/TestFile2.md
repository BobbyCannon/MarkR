### Header

```
public IEnumerable<Person> Get()
{
    var parent = new Person { Name = "John Smith" };
    var child1 = new Person { Name = "Bob Smith", Parent = parent };
    var child2 = new Person { Name = "Suzy Smith", Parent = parent };
    parent.Children = new[] { child1, child2 };

    return new[] { parent, child1, child2 };
}
```

More text here...