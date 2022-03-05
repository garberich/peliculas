export interface actorCreactionDTO {
    nombre: string;
    fechaNacimiento?: Date;
    foto?: File;
    fotoURL?: string;
    biografia?: string;
}

export interface actorPeliculasDTO{
    id: number;
    nombre: string;
    personaje: string;
    foto: string;
}