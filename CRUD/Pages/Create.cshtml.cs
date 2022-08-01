using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CRUD.Pages
{
    public class CreateModel : PageModel
    {
        public ListKendaraan listKendaraan = new ListKendaraan();
        public String errMsg = "";
        public String sccsMsg = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            listKendaraan.nomorRegistrasi = Request.Form["noRegistrasiKendaraan"];
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


            //insert data

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=CRUD;Persist Security Info=True;User ID=Admin;Password=admin";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = String.Format("INSERT INTO tb_kendaraan (Nomor_Registrasi_Kendaraan,Nama_Pemilik,Alamat,Merk,Tahun_Pembuatan,Kapasitas,Warna_Kendaraan,Bahan_Bakar) VALUES ('{0}','{1}','{2}','{3}',{4},{5},'{6}','{7}')", listKendaraan.nomorRegistrasi, listKendaraan.namaPemilik, listKendaraan.alamat, listKendaraan.merkKendaraan, listKendaraan.tahun, listKendaraan.kapasitas, listKendaraan.warna, listKendaraan.bahanBakar);

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return;
            }

            sccsMsg = "Data berhasil ditambahkan";
            Response.Redirect("/Index");
        }
    }
}
