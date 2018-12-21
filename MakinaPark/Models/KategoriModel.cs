using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Vegatro.Database;
using Vegatro.NetCore.Utils;

namespace MakinaPark.Models
{
    public class KategoriModel
    {
        public long Id { get; set; }
        public string Kategori { get; set; }
        public string KategoriAlt { get; set; }
        public string Aciklama { get; set; }
        public string Slug { get; set; }
        public string KategoriSlug { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public static KategoriModel Parse(DataRow row)
        {
            return new KategoriModel
            {
                Id = row.GetLong("Id"),
                Kategori = row.GetString("KategoriAd"),
                KategoriAlt = row.GetString("KategoriAltAd"),
                Aciklama = row.GetString("Aciklama"),
                Slug = row.GetString("Slug"),
                KategoriSlug = row.GetString("KategoriSlug"),
                KategoriAltList = new List<KategoriModel>(),
                KategoriMarkaList = new List<KategoriModel>(),
                KategoriModelList = new List<KategoriModel>()
            };
        }
        public List<KategoriModel> KategoriAltList { get; set; }

        public List<KategoriModel> KategoriMarkaList { get; set; }

        public List<KategoriModel> KategoriModelList { get; set; }

        public static List<KategoriModel> Listesi()
        {
            return Sql.GetInstance().List("sp_kategori_listesi", new List<object> { }, (row) =>
            {
                return Parse(row);
            });
        }


        public static List<KategoriModel> KategoriAltKiralikList(string slug)
        {
            List<KategoriModel> result = new List<KategoriModel>();

            Sql.GetInstance().Set("sp_kategorialt_listesi", new List<object> { slug }, (ds) =>
            {
                if (ds.Tables.Count <= 0)
                    return;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    KategoriModel kategori = Parse(row);

                    if (ds.Tables.Count <= 1)
                        continue;

                    foreach (DataRow subRow in ds.Tables[1].Rows)
                    {
                        KategoriModel altKategori = Parse(subRow);

                        if (!kategori.Slug.Equals(altKategori.KategoriSlug))
                            continue;

                        kategori.KategoriAltList.Add(altKategori);
                    }

                    result.Add(kategori);
                }
            });

            return result;
            //return kategori;
        }


        public static List<KategoriModel> KategoriMarkaKiralikList(string slug, string ustSlug)
        {
            List<KategoriModel> result = new List<KategoriModel>();

            Sql.GetInstance().Set("sp_kategorimarka_listesi", new List<object> { ustSlug }, (ds) =>
            {
                if (ds.Tables.Count <= 0)
                    return;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    KategoriModel kategori = Parse(row);

                    if (ds.Tables.Count <= 1)
                        continue;

                    foreach (DataRow subRow in ds.Tables[1].Rows)
                    {
                        KategoriModel altKategori = Parse(subRow);

                        if (!kategori.Slug.Equals(altKategori.KategoriSlug))
                            continue;


                        if (ds.Tables.Count <= 2)
                            continue;

                        foreach (DataRow subRow2 in ds.Tables[2].Rows)
                        {
                            KategoriModel marka = Parse(subRow2);

                            if (!altKategori.Slug.Equals(slug))
                                continue;

                            altKategori.KategoriMarkaList.Add(marka);
                        }
                        kategori.KategoriAltList.Add(altKategori);
                    }

                    result.Add(kategori);
                }
            });

            return result;
            //return kategori;
        }


        //internal static object KategoriAltSatilikList(string slug)
        //{
        //    throw new NotImplementedException();
        //}




        //internal static object KategoriMarkaSatilikList(string slug, string ustSlug)
        //{
        //    throw new NotImplementedException();
        //}




    }
}
