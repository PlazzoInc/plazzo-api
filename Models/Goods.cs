public class Goods
{
    public int Id { get; set; }
    public int Agency_Id { get; set; }
    public int User_Id { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public string Deed { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public double Surface { get; set; }
    public tinyint Rooms { get; set; }
    public tinyint Bathrooms { get; set; }
    public tinyint Bedrooms { get; set; }
    public tinyint Floor { get; set; }
    public DateTime Construction_Date { get; set; }
    public Char DPE { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }

}