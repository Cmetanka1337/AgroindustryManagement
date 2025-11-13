using System.Collections;
using System.Reflection;
using AgroindustryManagement.Services.Database;

namespace AgroindustryManagement.Services.Helpers;

public class DataCollector
{
    private readonly AGDatabaseService _databaseService;

    public DataCollector(AGDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    // TODO: Add check not only for lists but for other complex types as well
    public T CollectData<T>() where T : new()
    {
        var model = new T();
        foreach (var property in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (!property.CanWrite || property.PropertyType == typeof(DateTime))
            {
                Console.WriteLine($"Skipping property: {property.Name}");
                continue;
            }

            if (property.PropertyType.IsEnum)
            {
                HandleEnumProperty(property, model);
            }
            else if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string))
            {
                HandleListProperty(property, model);
            }
            else if (property.PropertyType == typeof(string) || property.PropertyType.IsValueType)
            {
                HandleSimpleProperty(property, model);
            }
            else
            {
                Console.WriteLine($"Skipping property {property.Name} (type: {property.PropertyType.Name})");
            }
        }
        return model;
    }

    private void HandleEnumProperty<T>(PropertyInfo property, T model)
    {
        Console.WriteLine($"Select value for {property.Name}:");
        var enumValues = Enum.GetValues(property.PropertyType);
        foreach (var value in enumValues)
        {
            Console.WriteLine($"{(int)value}. {value}");
        }

        int choice = ValidateIntegerInput("Enter your choice: ", value => Enum.IsDefined(property.PropertyType, value));
        property.SetValue(model, Enum.ToObject(property.PropertyType, choice));
    }

    private void HandleListProperty<T>(PropertyInfo property, T model)
    {
        var itemType = property.PropertyType.GenericTypeArguments[0];
        var fetchMethod = typeof(AGDatabaseService).GetMethod($"GetAll{itemType.Name}s");
        if (fetchMethod != null)
        {
            var relatedEntities = (IEnumerable)fetchMethod.Invoke(_databaseService, null);

            if (!relatedEntities.Cast<object>().Any())
            {
                Console.WriteLine($"No available {itemType.Name}s. Initializing {property.Name} as an empty list.");
                property.SetValue(model, Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType)));
                return;
            }

            Console.WriteLine($"Available {itemType.Name}s:");
            foreach (var entity in relatedEntities)
            {
                var idProperty = entity.GetType().GetProperty("Id");
                if (idProperty != null)
                {
                    Console.WriteLine($"Id: {idProperty.GetValue(entity)}");
                }
            }

            Console.WriteLine($"Enter the IDs of the {itemType.Name}s to add to {property.Name}, separated by commas:");
            var input = Console.ReadLine();
            var selectedIds = ValidateListInput(input, id => int.TryParse(id, out _));

            var selectedEntities = Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType)) as IList;
            foreach (var entity in relatedEntities)
            {
                var idProperty = entity.GetType().GetProperty("Id");
                if (idProperty != null && selectedIds.Contains((int)idProperty.GetValue(entity)))
                {
                    selectedEntities?.Add(entity);
                }
            }

            property.SetValue(model, selectedEntities);
        }
        else
        {
            Console.WriteLine($"No method found to fetch {itemType.Name}s. Skipping property {property.Name}.");
        }
    }

    private void HandleSimpleProperty<T>(PropertyInfo property, T model)
    {
        Console.WriteLine($"Enter value for {property.Name} ({property.PropertyType.Name}):");
        var input = Console.ReadLine();
        var convertedValue = ValidateAndConvertInput(input, property.PropertyType);
        if (convertedValue != null)
        {
            property.SetValue(model, convertedValue);
        }
    }

    private int ValidateIntegerInput(string prompt, Func<int, bool> validation)
    {
        int value;
        string? input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine();
        } while (!int.TryParse(input, out value) || !validation(value));

        return value;
    }

    private List<int> ValidateListInput(string input, Func<string, bool> validation)
    {
        var validIds = new List<int>();
        foreach (var id in input.Split(','))
        {
            if (validation(id.Trim()))
            {
                validIds.Add(int.Parse(id.Trim()));
            }
        }
        return validIds;
    }

    private object? ValidateAndConvertInput(string input, Type targetType)
    {
        try
        {
            return Convert.ChangeType(input, targetType);
        }
        catch
        {
            Console.WriteLine($"Invalid input. Expected type: {targetType.Name}. Skipping this property.");
            return null;
        }
    }
}