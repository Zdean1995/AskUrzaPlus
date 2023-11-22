using AskUrzaPlus.Models;
using SQLite;

namespace AskUrzaPlus;
public class UrzaRepository(string dbPath)
{
    string _dbPath = dbPath;

    public string StatusMessage { get; set; }
    private SQLiteAsyncConnection conn;

    private async Task Init()
    {
        if (conn != null)
        {
            return;
        }

        conn = new SQLiteAsyncConnection(_dbPath);
        await conn.CreateTableAsync<AbilityGroup>();
        await conn.CreateTableAsync<Ability>();
    }

    public async Task AddNewAbilityGroup(string name)
    {
        try
        {
            await Init();

            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");

            int result = await conn.InsertAsync(new AbilityGroup { Name = name });

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
        }
    }

    public async Task UpdateAbilityGroup(AbilityGroup abilityGroup)
    {
        try
        {
            await Init();
            await conn.UpdateAsync(abilityGroup);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to update ability group. {0}", ex.Message);
        }
    }

    public async Task RemoveAbilityGroup(int id)
    {
        try
        {
            await Init();

            List<Ability> abilities = await GetAllAbilitiesForGroup(id);

            foreach (Ability ability in abilities)
            {
                await conn.DeleteAsync(new Ability { Id = ability.Id });
            }

            int result = await conn.DeleteAsync(new AbilityGroup { Id = id });

            StatusMessage = string.Format("{0} record(s) deleted (id: {1})", result, id);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to delete {0}. Error: {1}", id, ex.Message);
        }
    }

    public async Task AddNewAbility(string abilityText, int groupId, int abilityType)
    {
        try
        {
            await Init();

            if (string.IsNullOrEmpty(abilityText))
                throw new Exception("Valid ability text required");

            int result = await conn.InsertAsync(new Ability { AbilityGroupId = groupId, AbilityText = abilityText, AbilityType = abilityType });

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, abilityText);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", abilityText, ex.Message);
        }
    }

    public async Task<List<Ability>> GetAllAbilitiesForGroup(int id)
    {
        try
        {
            await Init();
            return await conn.Table<Ability>().Where(ability => ability.AbilityGroupId == id).ToListAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return [];
    }

    public async Task UpdateAbility(Ability ability)
    {
        try
        {
            await Init();
            await conn.UpdateAsync(ability);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to update ability. {0}", ex.Message);
        }
    }

    public async Task RemoveAbility(int id)
    {
        int result = 0;
        try
        {
            await Init();

            result = await conn.DeleteAsync(new Ability { Id = id });

            StatusMessage = string.Format("{0} record(s) deleted (id: {1})", result, id);

        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to delete {0}. Error: {1}", id, ex.Message);
        }
    }
}
