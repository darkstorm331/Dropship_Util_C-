using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Util;
using eBay.Service.Core.Soap;


namespace TekBoxDropshipUtil
{
    class Program
    {
        static string exeDir = Environment.CurrentDirectory;
        static string csvDir = exeDir + @"\csvFiles";
        static string listingsDir = exeDir + @"\listings";
        static string csvFile;
        //static string endpoint = "https://api.sandbox.ebay.com/wsapi"; //Sandbox
        static string endpoint = "https://api.ebay.com/wsapi"; //Live
        static string callName = "AddFixedPriceItem";
        static string siteId = "0";
        //static string appId = "MichaelB-Dropship-SBX-12461717c-2e424876";// Sandbox
        static string appId = "MichaelB-Dropship-PRD-12466ad44-75575f45"; //Live
        static string devId = "3e594c39-b675-4727-9609-7e85c8252acd";     // Both
        //static string certId = "SBX-2461717c9245-4538-48ba-a3f5-7ac2";   // Sandbox
        static string certId = "PRD-2466ad4422cd-5683-486d-be74-f361"; //Live
        //static string authToken = "AgAAAA**AQAAAA**aAAAAA**CYVNWQ**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6wFk4GkC5OLpgqdj6x9nY+seQ**viIEAA**AAMAAA**qFRJqYqlgkDL5DPnKnny2RHKXPUC+WsuRPTJw60mFQSAUOoG238ccX1ufhk4vgnHVvr6faqkEqA+CJ9rIWbEk+MdGkJLdoOqwe6oYJEncTsrTN3jm0unsmAXkWVkkHSW2rOU+XI14FfsT89xiNeMHo0yN/Rs29LQ5yA5/PykktoZaqvja+T4/Y79+3W6E5RYragSyF1bn2GuNNGHAuYU5dOxw/gjFKhCRkNwMl73YGDueyAhyvH4d+eeLLTZ0HQ8/lRR2dfDyeAi2ye65f/bE7U5Av/ZawELoPP66B6awG3wqmOR5nHYBLcpGwZ6VZAqGHzVOBhy0Y8hZwTeNN+pZO9hEx2++bcL/Zt/cKkI5x+nUzn+WEoqGRdUGb9AtFYIqHTBxmzO3Is//kIJbfqFzyWw5t09wXgqBem/HmIeFVpw/z+Ry2KHndWIJxk7rmpqfzh69j2VkyJBGiEWoFVH8wyMyCCHywRO6yyx5rCSLuLOvtNuxOEe4HX9nJ+rbxUVO76Zb38wy0tnIYNtzw7wdxEdhU2hCihViuGtMICT3CZjrUJxmPa1g7EnPBz1mdf6CpIFTZdei0GbvVBoW4M6McP5e5AbjP56cT4pYfcyH+WKN7gYLJEI/Om/49nT3fjUSdauuqtZCjlrrCMs33S4rIgba3f6FHbBtj+Fnb1g2wPMCn50hgnCigQvldK7ac6XJwOKKbw9w8cz2uN5PFmJ7tqMBhXQfq+uOBxSFdxd3npH1N1WD/zkgn22ZAsep6rW"; //Sandbox
        static string authToken = "AgAAAA**AQAAAA**aAAAAA**M8tPWQ**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6wNkYajAZmDogWdj6x9nY+seQ**ZqkDAA**AAMAAA**K4ax1A9Ftf35mvLbHjt0HG2lL6hbt0IGOeJon1+pmu1pdb+uGAGFkDBZVZNh/AgNpJhGu7PBb2r9v788cMDVaBqpUbEqiefjUquXp+oexcuSr4G1lR3vvbSppm5HeXVkBeBXhNSzJoMNxjU4VOImxF6GLiiI6Z9YFUdqu35OHwBVRQRLIEHuwqkj899PBK8WRNtRakZ6zOvm0RcGJlcKj6xQFWkk5mRoY1vNyhYi8L29WUt8X9qXCq/5sjOOVDxnZqDKNhwzoBANxnn/qED1H+sEpHO7zVvZ9Pv7XXkQ+1SOhZ784PGxD7eX5+pIxd9S5rSVPB68UHY2dw2ncaWyA1e9dTu0MWh5xzOgIOomAuLsb34lz4W6qaG0FKw5D7MGuVwYr3Dpq3FJrmyZJEc5YTGqYFHrKq1xhflJRdutO9g6tEU2divItmq7WSgi5jozEIDj0h0P9QtWzWfPDokDK9+wvbvbc/w4JWHlVEfBYpRiEblE4TqmzWT2hn3W6ZdLj9QiZjVZn5l0nS4Wbpj+y8YM9ih0/S8+uhl0EdKQyQqPHVqj/IeTpkLvrUfOKxnPGJIhJkSn8fWS4WEo6dfu5UaX7RvsLzIPxwMiRr7Qe2xU2MGX8siRmhxxB8s7kkY3gL6VzuaKP1BKusYcDqm9hD6u/MDlr8tVIgLybrzxZRrCL2VnG4tOEca1+WpY3obTBulDHrRldKr35MbU/YiOpHa+Sa3oYmLmYTo34giFRN7KILI16pZ3gJI6BuzYzMv6"; //Live
        static string version = "981";
        static string secondClassRoyalMail = "UK_RoyalMailSecondClassRecorded"; //Under 2kg
        static string threeDayCourier = "UK_OtherCourier3Days"; //2kg to 30kg. Extra £3 per 30kg


        static void Main(string[] args)
        {
            if (!Directory.Exists(csvDir))
            {
                Directory.CreateDirectory(csvDir);
            }

            if (!Directory.Exists(listingsDir))
            {
                Directory.CreateDirectory(listingsDir);
                File.Create(listingsDir + @"\currentListings.txt");
            }

            List<string> currentListings = File.ReadAllLines(listingsDir + @"\currentListings.txt").ToList();

            DownloadCsvFromUrl("https://tbtrade.co.uk//feeds/customer_products.csv", "customer_products.csv");

            List<Product> productList = File.ReadAllLines(csvFile).Select(v => Product.FromCsv(v)).ToList();

            ApiContext context = new ApiContext();
            context.ApiCredential.eBayToken = authToken;
            context.ApiCredential.ApiAccount.Application = appId;
            context.ApiCredential.ApiAccount.Certificate = certId;
            context.ApiCredential.ApiAccount.Developer = devId;
            context.SoapApiServerUrl = endpoint;
            context.Version = version;
            context.Site = SiteCodeType.UK;

            foreach(Product product in productList)
            {
                if (currentListings.Contains(product.GetGtin().Replace("\"","").Trim()))
                {
                    ReviseFixedPriceItemCall reviseItem = new ReviseFixedPriceItemCall(context);
                    ItemType item2 = new ItemType();

                    double quantity = 0;
                    double price = 0;

                    try
                    {
                        quantity = double.Parse(product.GetStockLevel().Replace("\"", "").Trim());
                        price = double.Parse(product.GetPriceOne().Replace("\"", "").Replace("GBP", "").Trim());
                    } catch(Exception e)
                    {
                        continue;
                    }


                    if (int.Parse(quantity.ToString()) == 0)
                    {
                        EndFixedPriceItemCall endListing = new EndFixedPriceItemCall(context);
                        endListing.SKU = product.GetSku().Replace("\"", "").Trim();
                        endListing.EndingReason = EndReasonCodeType.NotAvailable;
                        endListing.Execute();
                        Console.Out.WriteLine(endListing.ApiResponse.Ack + " " + endListing.ApiResponse.SKU);
                        currentListings.Remove(product.GetGtin().Replace("\"", "").Trim());
                        currentListings.Add(Environment.NewLine);
                        File.WriteAllLines(listingsDir + @"\currentListings.txt", currentListings);
                        continue;
                    }

                    item2.SKU = product.GetSku().Replace("\"", "").Trim();

                    if(int.Parse(quantity.ToString()) < 5)
                    {
                        item2.Quantity = int.Parse(quantity.ToString());
                    } else if (int.Parse(quantity.ToString()) > 5)
                    {
                        item2.Quantity = 5;
                    }
 
                    item2.StartPrice = new AmountType();
                    item2.StartPrice.currencyID = CurrencyCodeType.GBP;
                    item2.StartPrice.Value = ((price*1.2) * 2);

                    item2.ShippingDetails = new ShippingDetailsType();
                    item2.ShippingDetails.ShippingServiceOptions = new ShippingServiceOptionsTypeCollection();
                    ShippingServiceOptionsType shipservice2 = new ShippingServiceOptionsType();

                    try
                    {
                        double doubleWeight = double.Parse(product.GetWeight().Replace("\"", "").Trim());

                        if (doubleWeight < 2000)
                        {
                            shipservice2.ShippingService = secondClassRoyalMail;
                            shipservice2.ShippingServicePriority = 1;
                            shipservice2.ShippingServiceCost = new AmountType();
                            shipservice2.ShippingServiceCost.currencyID = CurrencyCodeType.GBP;
                            shipservice2.ShippingServiceCost.Value = 2.99 * 1.20;
                            item2.ShippingDetails.ShippingServiceOptions.Add(shipservice2);
                        }
                        else if (doubleWeight > 2000 && doubleWeight < 30000)
                        {
                            shipservice2.ShippingService = threeDayCourier;
                            shipservice2.ShippingServicePriority = 1;
                            shipservice2.ShippingServiceCost = new AmountType();
                            shipservice2.ShippingServiceCost.currencyID = CurrencyCodeType.GBP;
                            shipservice2.ShippingServiceCost.Value = 4.99 * 1.20;
                            item2.ShippingDetails.ShippingServiceOptions.Add(shipservice2);
                        }
                        else
                        {
                            shipservice2.ShippingService = threeDayCourier;
                            shipservice2.ShippingServicePriority = 1;
                            shipservice2.ShippingServiceCost = new AmountType();
                            shipservice2.ShippingServiceCost.currencyID = CurrencyCodeType.GBP;
                            shipservice2.ShippingServiceCost.Value = 10;
                            item2.ShippingDetails.ShippingServiceOptions.Add(shipservice2);
                        }
                    }
                    catch (Exception e)
                    {
                        continue;
                    }

                    try
                    {
                        reviseItem.Item = item2;
                        reviseItem.Execute();
                        Console.Out.WriteLine(reviseItem.ApiResponse.Ack + " " + reviseItem.ApiResponse.SKU);
                    } catch(Exception e)
                    {

                    }
 
                    continue;
                } else
                {
                    AddFixedPriceItemCall itemCall = new AddFixedPriceItemCall(context);
                    itemCall.AutoSetItemUUID = true;

                    ItemType item = new ItemType();
                    item.ConditionID = 1000;
                    item.Country = CountryCodeType.GB;
                    item.Currency = CurrencyCodeType.GBP;
                    item.InventoryTrackingMethod = InventoryTrackingMethodCodeType.SKU;

                    try
                    {
                        item.SKU = product.GetSku().Replace("\"", "").Trim();
                        item.Description = product.GetDescription().Replace("\"", "").Trim();
                        item.Title = product.GetTitle().Replace("\"", "").Trim();
                    }
                    catch (Exception e)
                    {
                        continue;
                    }

                    item.ListingDuration = "GTC";
                    item.PaymentMethods = new BuyerPaymentMethodCodeTypeCollection();
                    item.PaymentMethods.Add(BuyerPaymentMethodCodeType.PayPal);
                    item.PayPalEmailAddress = "michaelbrand@ntlworld.com";
                    item.PostalCode = "E14 3WW";
                    item.DispatchTimeMax = 3;
                    item.ShippingDetails = new ShippingDetailsType();
                    item.ShippingDetails.ShippingServiceOptions = new ShippingServiceOptionsTypeCollection();
                    ShippingServiceOptionsType shipservice1 = new ShippingServiceOptionsType();

                    try
                    {
                        double doubleWeight = double.Parse(product.GetWeight().Replace("\"", "").Trim());

                        if (doubleWeight < 2000)
                        {
                            shipservice1.ShippingService = secondClassRoyalMail;
                            shipservice1.ShippingServicePriority = 1;
                            shipservice1.ShippingServiceCost = new AmountType();
                            shipservice1.ShippingServiceCost.currencyID = CurrencyCodeType.GBP;
                            shipservice1.ShippingServiceCost.Value = 2.99*1.20;
                            item.ShippingDetails.ShippingServiceOptions.Add(shipservice1);
                        }
                        else if(doubleWeight > 2000 && doubleWeight < 30000)
                        {
                            shipservice1.ShippingService = threeDayCourier;
                            shipservice1.ShippingServicePriority = 1;
                            shipservice1.ShippingServiceCost = new AmountType();
                            shipservice1.ShippingServiceCost.currencyID = CurrencyCodeType.GBP;
                            shipservice1.ShippingServiceCost.Value = 4.99*1.20;
                            item.ShippingDetails.ShippingServiceOptions.Add(shipservice1);
                        } else
                        {
                            shipservice1.ShippingService = threeDayCourier;
                            shipservice1.ShippingServicePriority = 1;
                            shipservice1.ShippingServiceCost = new AmountType();
                            shipservice1.ShippingServiceCost.currencyID = CurrencyCodeType.GBP;
                            shipservice1.ShippingServiceCost.Value = 10;
                            item.ShippingDetails.ShippingServiceOptions.Add(shipservice1);
                        }
                    }
                    catch (Exception e)
                    {
                        continue;
                    }

                    //Specify Return Policy
                    item.ReturnPolicy = new ReturnPolicyType();
                    item.ReturnPolicy.ReturnsAcceptedOption = "ReturnsAccepted";

                    try
                    {
                        double quantity = double.Parse(product.GetStockLevel().Replace("\"", "").Trim());
                        double price = double.Parse(product.GetPriceOne().Replace("\"", "").Replace("GBP", "").Trim());

                        if (int.Parse(quantity.ToString()) == 0)
                        {
                            continue;
                        } 

                        if(int.Parse(quantity.ToString()) > 5)
                        {
                            item.Quantity = 5;
                        } else
                        {
                            item.Quantity = int.Parse(quantity.ToString());
                        }
                        
                        item.StartPrice = new AmountType();
                        item.StartPrice.currencyID = CurrencyCodeType.GBP;
                        item.StartPrice.Value = ((price*1.2) * 2);

                    }
                    catch (Exception e)
                    {
                        continue;
                    }

                    GetSuggestedCategoriesCall suggested = new GetSuggestedCategoriesCall(context);
                    suggested.Query = product.GetTitle().Replace("\"", "").Trim();
                    suggested.Execute();

                    if (suggested.SuggestedCategoryList != null)
                    {
                        string categoryName = suggested.SuggestedCategoryList[0].Category.CategoryID;
                        item.PrimaryCategory = new CategoryType();
                        item.PrimaryCategory.CategoryID = categoryName;
                        
                    }
                    else
                    {
                        item.PrimaryCategory = new CategoryType();
                        item.PrimaryCategory.CategoryID = "181076";
                    }




                    item.ProductListingDetails = new ProductListingDetailsType();

                    //Specifying UPC as the product identifier. Other applicable product identifiers
                    //include ISBN, EAN, Brand-MPN.

                    try
                    {
                        item.ProductListingDetails.EAN = product.GetGtin().Replace("\"", "").Trim();
                    }
                    catch (Exception e)
                    {

                    }


                    //If multiple product identifiers are specified, eBay uses the first one that
                    //matches a product in eBay's catalog system.

                    item.ProductListingDetails.UseFirstProduct = true;
                        
                    item.ProductListingDetails.BrandMPN = new BrandMPNType();
                    item.ProductListingDetails.BrandMPN.Brand = "Tekbox";
                    item.ProductListingDetails.BrandMPN.MPN = product.GetMpn().Replace("\"", "").Trim();
                    NameValueListType brands = new NameValueListType();
                    NameValueListType MPN = new NameValueListType();
                    StringCollection brandSC = new StringCollection();
                    StringCollection MPNSC = new StringCollection();

                    item.ItemSpecifics = new NameValueListTypeCollection();

                    brandSC.Add("Tekbox");
                    MPNSC.Add(product.GetMpn().Replace("\"", "").Trim());
                    brands.Name = "Brand";
                    MPN.Name = "MPN";
                    brands.Value = brandSC;
                    MPN.Value = MPNSC;

                    item.ItemSpecifics.Add(brands);
                    item.ItemSpecifics.Add(MPN);
                       

                    //For listing to be pre-filled with product information from the catalog
                    item.ProductListingDetails.IncludeeBayProductDetails = true;

                    //Include the eBay stock photo with the listing if available and use it as the gallery picture
                    item.ProductListingDetails.IncludeStockPhotoURL = true;
                    item.ProductListingDetails.UseStockPhotoURLAsGallery = true;
                    item.ProductListingDetails.UseStockPhotoURLAsGallerySpecified = true;

                    //If multiple prod matches found, list the item with the 1st product's information
                    item.ProductListingDetails.UseFirstProduct = true;

                    //Add pictures
                    item.PictureDetails = new PictureDetailsType();
                    item.PictureDetails.ExternalPictureURL = new StringCollection();

                    try
                    {
                        if (product.GetImageLinkOne().Contains("https://"))
                        {
                            item.PictureDetails.ExternalPictureURL.Add(product.GetImageLinkOne().Replace("\"", "").Trim());
                        }
                    }
                    catch (Exception e)
                    {

                    }

                    try
                    {
                        if (product.GetImageLinkTwo().Contains("https://"))
                        {
                            item.PictureDetails.ExternalPictureURL.Add(product.GetImageLinkTwo().Replace("\"", "").Trim());
                        }
                    }
                    catch (Exception e)
                    {

                    }

                    try
                    {
                        if (product.GetImageLinkThree().Contains("https://"))
                        {
                            item.PictureDetails.ExternalPictureURL.Add(product.GetImageLinkThree().Replace("\"", "").Trim());
                        }
                    }
                    catch (Exception e)
                    {

                    }

                    try
                    {
                        if (product.GetImageLinkFour().Contains("https://"))
                        {
                            item.PictureDetails.ExternalPictureURL.Add(product.GetImageLinkFour().Replace("\"", "").Trim());
                        }
                    }
                    catch (Exception e)
                    {

                    }

                    try
                    {
                        if (product.GetImageLinkFive().Contains("https://"))
                        {
                            item.PictureDetails.ExternalPictureURL.Add(product.GetImageLinkFive().Replace("\"", "").Trim());
                        }
                    }
                    catch (Exception e)
                    {

                    }

                    //Specify GalleryType
                    item.PictureDetails.GalleryType = GalleryTypeCodeType.None;
                    item.PictureDetails.GalleryTypeSpecified = true;

                    itemCall.Item = item;

                    try
                    {
                        itemCall.Execute();
                        Console.Out.WriteLine(itemCall.ApiResponse.Ack + " " + itemCall.ApiResponse.SKU);
                        File.AppendAllText(listingsDir + @"\currentListings.txt", product.GetGtin().Replace("\"","").Trim() + Environment.NewLine);
                    }
                    catch (Exception e)
                    {
                        Console.Out.WriteLine(itemCall.ApiException + Environment.NewLine);
                        continue;
                    }
                }              
            }
            Console.Out.WriteLine("Finished");
            //Console.In.Read();
        }



        static void DownloadCsvFromUrl(String url, String fileName)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(url, csvDir + @"\" + fileName);
                csvFile = csvDir + @"\" + fileName;
            }
        }
    }

    class Product
    {
        String sku;
        String title;
        String stock_level;
        String price_1;
        String price_5plus;
        String price_10plus;
        String price_20plus;
        String weight;
        string brand;
        string mpn;
        string gtin;
        string link;
        string description;
        string image_link_1;
        string image_link_2;
        string image_link_3;
        string image_link_4;
        string image_link_5;

        public static Product FromCsv(string csvLine)
        {
            string[] values = csvLine.Split('\t');
            Product product = new Product();

            try
            {
                product.sku = values[0];
            } catch(Exception e)
            {
                product.sku = "";
            }

            try
            {
                product.title = values[1];
            }
            catch (Exception e)
            {
                product.title = "";
            }

            try
            {
                product.stock_level = values[2];
            }
            catch (Exception e)
            {
                product.stock_level = "0";
            }

            try
            {
                product.price_1 = values[3];
            }
            catch (Exception e)
            {
                product.price_1 = "0";
            }

            try
            {
                product.price_5plus = values[4];
            }
            catch (Exception e)
            {
                product.price_5plus = "0";
            }

            try
            {
                product.price_10plus = values[5];
            }
            catch (Exception e)
            {
                product.price_10plus = "0";
            }

            try
            {
                product.price_20plus = values[6];
            }
            catch (Exception e)
            {
                product.price_20plus = "0";
            }

            try
            {
                product.weight = values[7];
            }
            catch (Exception e)
            {
                product.weight = "0";
            }

            try
            {
                product.brand = values[8];
            }
            catch (Exception e)
            {
                product.brand = "";
            }

            try
            {
                product.mpn = values[9];
            }
            catch (Exception e)
            {
                product.mpn = "";
            }

            try
            {
                product.gtin = values[10];
            }
            catch (Exception e)
            {
                product.gtin = "";
            }

            try
            {
                product.link = values[11];
            }
            catch (Exception e)
            {
                product.link = "";
            }

            try
            {
                product.description = values[12];
            } catch (Exception e)
            {
                product.description = "";
            }

            try
            {
                product.image_link_1 = values[13];
            }
            catch (Exception e)
            {
                product.image_link_1 = "";
            }

            try
            {
                product.image_link_2 = values[14];
            }
            catch (Exception e)
            {
                product.image_link_2 = "";
            }

            try
            {
                product.image_link_3 = values[15];
            }
            catch (Exception e)
            {
                product.image_link_3 = "";
            }

            try
            {
                product.image_link_4 = values[16];
            }
            catch (Exception e)
            {
                product.image_link_4 = "";
            }

            try
            {
                product.image_link_5 = values[17];
            }
            catch (Exception e)
            {
                product.image_link_5 = "";
            }

            return product;
        }

        public String GetSku()
        {
            return sku;
        }

        public String GetTitle()
        {
            return title;
        }

        public String GetStockLevel()
        {
            return stock_level;
        }

        public String GetPriceOne()
        {
            return price_1;
        }

        public String GetPriceFivePlus()
        {
            return price_5plus;
        }

        public String GetPriceTenPlus()
        {
            return price_10plus;
        }

        public String GetPriceTwentyPlus()
        {
            return price_20plus;
        }

        public String GetWeight()
        {
            return weight;
        }

        public String GetBrand()
        {
            return brand;
        }

        public String GetMpn()
        {
            return mpn;
        }

        public String GetGtin()
        {
            return gtin;
        }

        public String GetLink()
        {
            return link;
        }

        public String GetDescription()
        {
            return description;
        }

        public String GetImageLinkOne()
        {
            return image_link_1;
        }

        public String GetImageLinkTwo()
        {
            return image_link_2;
        }

        public String GetImageLinkThree()
        {
            return image_link_3;
        }

        public String GetImageLinkFour()
        {
            return image_link_4;
        }

        public String GetImageLinkFive()
        {
            return image_link_5;
        }
    }
}
