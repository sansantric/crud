using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CRUD.Pages
{
    public class IndexModel : PageModel
    {
        public List<ListKendaraan> ListKendaraan = new List<ListKendaraan>();
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }


        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=CRUD;Persist Security Info=True;User ID=Admin;Password=admin";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT Nomor_Registrasi_Kendaraan, Nama_Pemilik, Merk, Tahun_Pembuatan, Kapasitas , Warna_Kendaraan, Bahan_Bakar FROM tb_kendaraan";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            int i = 1;
                            while (reader.Read())
                            {
                                ListKendaraan listKendaraan = new ListKendaraan();
                                listKendaraan.id = i;
                                listKendaraan.nomorRegistrasi = reader.GetString(0);
                                listKendaraan.namaPemilik = reader.GetString(1);
                                listKendaraan.merkKendaraan = reader.GetString(2);
                                listKendaraan.tahun = reader.GetInt32(3);
                                listKendaraan.kapasitas = reader.GetInt32(4).ToString() + " CC";
                                listKendaraan.warna = reader.GetString(5);
                                listKendaraan.bahanBakar = reader.GetString(6);
                                i++;
                                ListKendaraan.Add(listKendaraan);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: "+ ex.Message.ToString());
            }
        }
    }

    public class ListKendaraan
    {
        public int id;
        public String  nomorRegistrasi;
        public String namaPemilik;
        public String alamat;
        public String merkKendaraan;
        public int tahun;
        public String kapasitas;
        public String warna;
        public String bahanBakar;
    }
}