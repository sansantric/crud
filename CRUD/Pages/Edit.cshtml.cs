using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CRUD.Pages
{
    public class EditModel : PageModel
    {
        public ListKendaraan listKendaraan = new ListKendaraan();
        public String sccsMsg = "";
        public String errMsg = "";

        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=CRUD;Persist Security Info=True;User ID=Admin;Password=admin";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = String.Format("SELECT Nomor_Registrasi_Kendaraan, Nama_Pemilik, Alamat, Merk, Tahun_Pembuatan, Kapasitas , Warna_Kendaraan, Bahan_Bakar FROM tb_kendaraan WHERE Nomor_Registrasi_Kendaraan='{0}'", id);

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        System.Diagnostics.Debug.WriteLine(sql);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                listKendaraan.nomorRegistrasi = reader.GetString(0);
                                listKendaraan.namaPemilik = reader.GetString(1);
                                listKendaraan.alamat = reader.GetString(2);
                                listKendaraan.merkKendaraan = reader.GetString(3);
                                listKendaraan.tahun = reader.GetInt32(4);
                                listKendaraan.kapasitas = reader.GetInt32(5).ToString();
                                listKendaraan.warna = reader.GetString(6);
                                listKendaraan.bahanBakar = reader.GetString(7);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message.ToString());
            }
        }
        public void OnPost()
        {
            listKendaraan.nomorRegistrasi = Request.Form["id"];
            listKendaraan.namaPemilik = Request.Form["namaPemilik"];
            listKendaraan.alamat = Request.Form["alamat"];
            listKendaraan.merkKendaraan = Request.Form["merk"];
            listKendaraan.tahun = int.Parse(Request.Form["tahun"]);
            listKendaraan.kapasitas = Request.Form["kapasitas"];
            listKendaraan.warna = Request.Form["warna"];
            listKendaraan.bahanBakar = Request.Form["bahanBakar"];

            if (listKendaraan.nomorRegistrasi.Length == 0 ||
                listKendaraan.namaPemilik.Length == 0 ||
                listKendaraan.alamat.Length == 0 ||
                listKendaraan.merkKendaraan.Length == 0 ||
                listKendaraan.tahun == 0 ||
                listKendaraan.kapasitas.Length == 0 ||
                listKendaraan.warna.Length == 0 ||
                listKendaraan.bahanBakar.Length == 0)
            {

                errMsg = "Mohon lengkapi data";
                return;
            }
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=CRUD;Persist Security Info=True;User ID=Admin;Password=admin";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = String.Format("UPDATE tb_kendaraan SET Nama_Pemilik = '{0}' ,Alamat = '{1}' ,Merk = '{2}' ,Tahun_Pembuatan = {3} ,Kapasitas = {4},Warna_Kendaraan = '{5}' ,Bahan_Bakar = '{6}' WHERE Nomor_Registrasi_Kendaraan='{7}'", listKendaraan.namaPemilik, listKendaraan.alamat, listKendaraan.merkKendaraan, listKendaraan.tahun, listKendaraan.kapasitas, listKendaraan.warna, listKendaraan.bahanBakar,listKendaraan.nomorRegistrasi);
                    System.Diagnostics.Debug.WriteLine(sql);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message.ToString());
            }

            sccsMsg = "Data Berhasil di Update";
            Response.Redirect("/Index");
        }
    }
}
