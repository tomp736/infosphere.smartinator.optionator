
## Project Init

### Create Base Projects

``` bash
cd src
dotnet new classlib -n optionator.core
dotnet new mstest -n optionator.core.tests
dotnet new classlib -n optionator.data
dotnet new mstest -n optionator.data.tests
dotnet new webapi -n optionator.webapi
dotnet new mstest -n optionator.webapi.tests
```

### Add Project References

``` bash
cd optionator.data
dotnet add reference ../optionator.core
```

``` bash
cd optionator.data.tests
dotnet add reference ../optionator.core
dotnet add reference ../optionator.data
```

``` bash
cd optionator.webapi
dotnet add reference ../optionator.data
dotnet add reference ../optionator.core
```

### Add Test Projects

``` bash
cd src
dotnet new mstest -n optionator.core.tests
dotnet new mstest -n optionator.data.tests
dotnet new mstest -n optionator.webapi.tests
```

### Add Project References

``` bash
cd optionator.core.tests
dotnet add reference ../optionator.core
```

``` bash
cd optionator.data.tests
dotnet add reference ../optionator.core
dotnet add reference ../optionator.data
```

``` bash
cd optionator.webapi.tests
dotnet add reference ../optionator.core
dotnet add reference ../optionator.data
dotnet add reference ../optionator.webapi
```

## Docker Files