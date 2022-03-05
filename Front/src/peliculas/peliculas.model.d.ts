import { actorPeliculasDTO } from "../actores/actores.model";

export interface pelicula{
    id: number,
    titulo: string,
    poster: string
}

export interface peliculaCreacionDTO{
    titulo: string;
    enCines: boolean;
    trailer: string;
    fechaLanzamiento?: Date;
    poster?: File;
    posterURL?: string;
    generosIds?:number[];
    cinesIds?:number[];
    actores?: actorPeliculasDTO[];
}

export interface landingPageDTO {
    enCartelera?: pelicula[];
    proximosEstrenos?: pelicula[];
}