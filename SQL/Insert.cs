namespace DataLayer_LandingPage.SQL;

public class Insert
{
    private string sqlConnectionString = "abc";
    
    public static void InsertToSQL(uint providerID, uint practiceID, string recommendationList)
    {
        ConnectToSQL();
        InsertData(providerID, practiceID, recommendationList);
    }

    private static void InsertData(uint providerId, uint practiceId, string recommendationList)
    {
        throw new NotImplementedException();
    }

    private static void ConnectToSQL()
    {
        throw new NotImplementedException();
    }
    
    /*
     * Queries
     * CREATE TABLE ProviderInsuranceRecommendationsCampaign (
    ProfId bigint,
    PracticeId bigint,
    RecommendationList nvarchar(max),
);
     */
}