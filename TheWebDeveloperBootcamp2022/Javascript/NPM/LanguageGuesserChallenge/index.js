import { franc, francAll } from 'franc';
import langs from 'langs';
import { argv } from 'process';

const threeLetterCountry = franc(argv[2],{minLength:3});

const languageObje = langs.where("2", threeLetterCountry);
if (languageObje == null) {
    console.log(languageObje.name)
    console.log("Invalid input, please try again")
}
else {
    
    console.log(`${argv[2]} language is ${languageObje.name}`);
}
