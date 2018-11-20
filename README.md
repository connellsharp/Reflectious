# Reflectious

Reflectious simplifies, extends and optimises metaprogramming in C#. It combines techniques from Reflection, Expression Trees and Delegates.

## Fluent API

The Reflectors can all be accessed using the static `Reflect` class. From there, you can find members and invoke them.

**Invoke a method by name:**
```
Reflect.Instance(obj)
    .GetMethod("DoTheThing")
    .Invoke();
```

**Set a property from an expression:**
```
Reflect.Instance(stub)
    .GetProperty(s => s.InstanceProperty)
    .SetValue("Test change");
```

**Easily find a method overload:**
```
bool containsAdults = Reflect.Type(typeof(Enumerable))
    .GetMethod("Any")
    .MakeGeneric<Person>()
    .WithParameters<IEnumerable<Person>, Func<Person, bool>>()
    .Invoke(people, a => a.Age >= 18);
```

**Or find the extension and infer the types:**
```
bool containsAdults = Reflect.Instance(people)
    .GetExtensionMethod(typeof(Enumerable), "Any")
    .WithParameters<Func<Person, bool>>()
    .Invoke(a => a.Age >= 18);
```

**Resolve method parameters using IServiceProvider:**
```
Reflect.Type<UserService>()
    .WithNewInstance()
    .GetMethod("CreateUser")
    .WithParameters<IUserRepository, UserConfiguration>()
    .FromServiceProvider(serviceProvider)
    .Invoke();
```

**Even resolve the main instance using IServiceProvider:**
```
Reflect.Type<UserService>()
    .WithNewInstance()
    .UsingConstructor<IUserRepository, UserConfiguration>()
    .WithArgumentsFromServiceProvider(sp)
    .GetMethod("CreateUser")
    .Invoke(newUser);
```