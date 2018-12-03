## Install

- download or clone
- build
- apply EFCore migrations:

```
   $ update-database
```
- add Scalar function to database (used in filtering)
```sql
    USE HeadHunterClient
    GO

    ALTER FUNCTION dbo.ConvertToCurrency (@amount DECIMAL(18, 6), @code NVARCHAR(450))
    RETURNS MONEY
    BEGIN
      RETURN (@amount * (SELECT TOP 1
          Rate
        FROM Currency
        WHERE Code = @code)
      )
    END

    GO
```

