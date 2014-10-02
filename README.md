FateDeck
========

This is learning project that is currently under development about 16 hours. It is an encounter generator for the game of Malifaux. The goals of this project are to explore mobile first responsive design, Dapper Micro-ORM, and SQLite DB. 

Some interesting bits:

Dependency injection

src\FateDeck.Web\Runtime\ProductionModule.cs

Base Repository for Dapper

src\FateDeck.Web\Repositories\FateDeckRepositoryBase.cs

SQLIte DB Creation

src\FateDeck.Web\Runtime\DataSource.cs

View that fits small, medium and large screen sizes.

src\FateDeck.Web\Views\Home\Index.cshtml
