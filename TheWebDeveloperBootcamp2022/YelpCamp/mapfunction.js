var data = [
    {
        fieldname: 'image',
        originalname: '5580644.jpg',
        encoding: '7bit',
        mimetype: 'image/jpeg',
        path: 'https://res.cloudinary.com/kingofgodz/image/upload/v1646592628/YelpCamp/gtjsxduc7ztasewbyryq.jpg',
        size: 30364,
        filename: 'YelpCamp/gtjsxduc7ztasewbyryq'
    },
    {
        fieldname: 'image',
        originalname: 'de6036028032b8787f284f823926204f.jpg',
        encoding: '7bit',
        mimetype: 'image/jpeg',
        path: 'https://res.cloudinary.com/kingofgodz/image/upload/v1646592628/YelpCamp/gn1ifbuxlyetfxkmxdq9.jpg',
        size: 40348,
        filename: 'YelpCamp/gn1ifbuxlyetfxkmxdq9'
    }
]

console.log(data.map(result => ({ name:result.fieldname, url: result.path })))