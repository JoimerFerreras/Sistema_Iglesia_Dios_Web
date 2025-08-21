using Newtonsoft.Json;

public class Funcionalidad
{
    // Parte de la funcionalidad
    [JsonProperty("Id_Funcionalidad")]
    public int IdFuncionalidad { get; set; }

    [JsonProperty("Id_Modulo")]
    public int IdModulo { get; set; }

    [JsonProperty("Nombre_Funcionalidad")]
    public string NombreFuncionalidad { get; set; }

    [JsonProperty("Descripcion_Funcionalidad")]
    public string DescripcionFuncionalidad { get; set; }

    [JsonProperty("Nombre_Archivo")]
    public string NombreArchivo { get; set; }

    [JsonProperty("Clase_CSS_Funcionalidad")]
    public string Clase_CSS_Funcionalidad { get; set; }


    // Parte del modulo
    [JsonProperty("Nombre_Modulo")]
    public string NombreModulo { get; set; }

    [JsonProperty("Descripcion_Modulo")]
    public string DescripcionModulo { get; set; }

    [JsonProperty("Clase_CSS_Modulo")]
    public string Clase_CSS_Modulo { get; set; }

}
