import { useEffect, useState } from "react";
import ListadoPeliculas from "./peliculas/ListadoPeliculas";
import { landingPageDTO } from "./peliculas/peliculas.model";

export default function LandingPage() {
    const [peliculas, setPeliculas] = useState<landingPageDTO>({})

    useEffect(() => {
        const timerId = setTimeout(() => {
        setPeliculas({
            enCartelera: [
            {
                id: 1,
                titulo: "Spider-Man: Far from Home",
                poster: 'https://play-lh.googleusercontent.com/Zei0qLmw_UIfyv48C_vMbFNOaR8iduiK9qCl8xoZSi2utmtczoli0RJXyoUAYhXD_Vfz0qcdnZyxgVawRA'
            },
            {
                id: 2,
                titulo: "Moana",
                poster: 'https://www.planetadelibros.com.co/usuaris/libros/fotos/244/m_libros/portada_moana-la-novela_mattel_201612051759.jpg'
            }
            ],
            proximosEstrenos: [
            {
                id: 3,
                titulo: "Soul",
                poster: 'https://es.web.img3.acsta.net/pictures/20/03/24/14/59/1973735.jpg'
            }
            ]
        })
        }, 1000);

        return () => clearTimeout(timerId);
    })
    
    return (
        <>
            <h3>En Cartelera</h3>
            <ListadoPeliculas peliculas={peliculas.enCartelera} />

            <h3>Próximos Estrenos</h3>
            <ListadoPeliculas peliculas={peliculas.proximosEstrenos} />
        </>
    )
}