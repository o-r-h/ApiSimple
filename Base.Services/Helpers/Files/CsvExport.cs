using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;


namespace Base.Services.Helpers.Files
{
    /// <summary>
    /// Ejemplo de uso:
    ///  CsvExport<NombreClase> csv = new CsvExport<NombreClase>();
    ///  csv.DelimiterCharacter = ";";
    ///  csv.EncodingSet = Encoding.UTF8;
    ///  byte[] writeCsvToMemory = csv.WriteCsvToMemory(Aqui Va el IENUMERABLEde la clase);
    ///  
    ///  Decorador de la clase (En caso de necesitar otro nombre para la columna)
    ///  [CsvHelper.Configuration.Attributes.Name("Unidades disponibles")]
    ///  public string NroAccionesCompradas { get; set; }
    ///  
    ///  Enviar un IEnumerable con los datos
    ///  byte[] writeCsvToMemory = csv.WriteCsvToMemory(<IEnumerable>,CsvSetup);
    /// 
    /// </summary>
    /// <param name="records"></param>
    /// <returns></returns>
    public static class CsvExport<T>
    {

        public static byte[] WriteCsvToMemory(IEnumerable<T> records, string delimiterCharacter, System.Text.Encoding encodingSet)
        {
            var configCsv = new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = String.IsNullOrEmpty(delimiterCharacter) ? "," : delimiterCharacter, Encoding = encodingSet };
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter, configCsv))
            {
                csvWriter.WriteRecords(records);
                streamWriter.Flush();
                return memoryStream.ToArray();
            }
        }

    }
}
