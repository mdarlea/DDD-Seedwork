# DDD - Seedwork Application Blocks
MyGet<br>([feed](https://www.myget.org/F/ddd-seedwork/)) | ![#](https://img.shields.io/myget/ddd-seedwork/vpre/ddd-seedwork.svg?style=flat) | ![#](https://img.shields.io/myget/ddd-seedwork/vpre/ddd-seedwork.runner.dnx.svg?style=flat) | ![#](https://img.shields.io/myget/ddd-seedwork/vpre/ddd-seedwork.runner.visualstudio.svg?style=flat)


DDD-Seedwork is a set of lightweight Application Blocks with the goal of help on the development of NLayered DDD Applications. It is limited, of course, only to the "coding" part of a project related to Domain Driven Design (DDD) - code based on DDD patterns, does not mean having a DDD solution.

Martin Fowler has a very interesting post related to Seedwork http://www.martinfowler.com/bliki/Seedwork.html. Seedwork is the most possible reusable library, that can serve the maximum number of Apps Contexts. Framework, for us, will be libraries related to some specific approach or technology.

## [DDD Core Components](https://www.nuget.org/packages/Swaksoft.Core/) ![NuGet version](https://badge.fury.io/nu/Swaksoft.Core.png)
```
PM> Install-Package Swaksoft.Core
```

### [Domain Seedwork Application Block](https://www.nuget.org/packages/Swaksoft.Domain.Seedwork/) ![NuGet version](https://badge.fury.io/nu/Swaksoft.Domain.Seedwork.png)
```
PM> Install-Package Swaksoft.Domain.Seedwork
```

### [Application Seedwork Application Block](https://www.nuget.org/packages/Swaksoft.Application.Seedwork/) ![NuGet version](https://badge.fury.io/nu/Swaksoft.Application.Seedwork.png)
```
PM> Install-Package Swaksoft.Application.Seedwork
```
### Infrastructure Seedwork Application Blocks

#### [Crosscutting Seedwork Application Block](https://www.nuget.org/packages/Swaksoft.Infrastructure.Crosscutting/) ![NuGet version](https://badge.fury.io/nu/Swaksoft.Infrastructure.Crosscutting.png)
```
PM> Install-Package Swaksoft.Infrastructure.Crosscutting
```
- Logging
- Caching
- Validation
- Type mapping

#### [Authorization Crosscutting Application Block](https://www.nuget.org/packages/Swaksoft.Infrastructure.Crosscutting.Authorization/) ![NuGet version](https://badge.fury.io/nu/Swaksoft.Infrastructure.Crosscutting.Authorization.png)
```
PM> Install-Package Swaksoft.Infrastructure.Crosscutting.Authorization
```
#### [Communication Crosscutting Application Block](https://www.nuget.org/packages/Swaksoft.Infrastructure.Crosscutting.Communication/) ![NuGet version](https://badge.fury.io/nu/Swaksoft.Infrastructure.Crosscutting.Communication.png)
```
PM> Install-Package Swaksoft.Infrastructure.Crosscutting.Communication
```

### Data
- contains adapters for Entity Framwork and NHibernate

#### [EntityFramework Data Infrastructure Application Block](https://www.nuget.org/packages/Swaksoft.Infrastructure.Data.Seedwork/) ![NuGet version](https://badge.fury.io/nu/Swaksoft.Infrastructure.Data.Seedwork.png)
```
PM> Install-Package Swaksoft.Infrastructure.Data.Seedwork
```
#### [NHibernate Data Infrastructure Application Block](https://www.nuget.org/packages/Swaksoft.Infrastructure.Data.NHibernate.Seedwork/) ![NuGet version](https://badge.fury.io/nu/Swaksoft.Infrastructure.Data.NHibernate.Seedwork.png)
```
PM> Install-Package Swaksoft.Infrastructure.Data.NHibernate.Seedwork
```