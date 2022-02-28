const express = require("express");
const app = express();
const path = require("path");
const { v4: uuid } = require("uuid");
const methodOverride = require("method-override")
app.use(methodOverride("_method"));
app.use(express.urlencoded({ extended: true })) // for parsing application/x-www-form-urlencoded
app.set("views", path.join(__dirname, "views"))
app.set("view engine", "ejs");


let comments = [
    {
        id: uuid(),
        username: "Todd",
        comment: "lol that is funny"
    },
    {
        id: uuid(),
        username: "Dear",
        comment: "Hello world"
    },
    {
        id: uuid(),
        username: "Em",
        comment: "This is me!"
    }
]
let posts = [
    {
        id: uuid(),
        username: "Dear",
        content: "This is a post",
        comments:
            [
                {
                    id: uuid(),
                    username: "Em",
                    comment:"This is em"
                },
                {
                    id: uuid(),
                    username: "Dear",
                    comment:"This is dear",

                    
                }
            ]
    },
    {
        id: uuid(),
        username: "King",
        content: "This is a post2",
        comments:
            [
                {
                    id: uuid(),
                    username: "Em1",
                    comment: "This is em1"
                },
                {
                    id: uuid(),
                    username: "Dear1",
                    comment: "This is dear1",


                }
            ]
    }

]
//POST SECTION

app.get("/posts", (req, res) => {
    res.render("posts/posts.ejs", { posts: posts });
})
app.get("/posts/new", (req, res) => {
    res.render("posts/newPost.ejs");
})
app.post("/posts", (req, res) => {
    console.log(req.body)
    const { username, content } = req.body;
    
    let post = {
        id: uuid(),
        username: username,
        content: content,
        comments:[]
    }
    posts.push(post);
    res.redirect("/posts");
})
app.get("/posts/:id", (req, res) => {
    const { id } = req.params;
    const post = posts.find(p => (p.id === id));

    res.render("posts/detailsPost.ejs",{post:post})
})
app.get("/posts/:id/edit", (req, res) => {
    const { id } = req.params;
    const post = posts.find(p => (p.id === id));
    res.render("posts/editPost.ejs",{post:post});
})
app.patch("/posts/:id", (req, res) => {
    const { content } = req.body;
    const {id} = req.params
    const post = posts.find(p => (p.id === id));
    post.content = content;
    res.redirect("/posts");
})
app.delete("/posts/:id", (req, res) => {
    const { id } = req.params;
    posts = posts.filter(p => (p.id !== id));
    res.redirect("/posts");
})
//COMMENT SECTION WITHIN POST
app.get("/posts/:id/comments", (req, res) => {
    const { id } = req.params;
    const post = posts.find(p => (p.id === id));
    res.render("postComments/allComments.ejs",{post:post})
})
app.get("/posts/:id/comments/new", (req, res) => {
    const { id } = req.params;
    console.log(id);
    res.render("postComments/new.ejs",{id:id});

})
app.post("/posts/:id/comments", (req, res) => {
    const { comment, username } = req.body;
    const { id } = req.params;
    const newComment = {
        id: uuid(),
        username: username,
        comment:comment
    }
    const post = posts.find(p => (p.id === id));
    post.comments.push(newComment);
    res.redirect(`/posts/${post.id}`)
})
app.get("/posts/:id/comments/:commentid/edit", (req, res) => {
    const { id, commentid } = req.params;
    console.log(id, commentid);
    const postFound = posts.find(p => (p.id === id));
    const comment = postFound.comments.find(c => (c.id === commentid));
    console.log(comment);
    res.render("postComments/edit.ejs",{comment:comment,postID:id})
})
app.patch("/posts/:id/comments/:commentid", (req, res) => {
    const { id, commentid } = req.params;
    const { comment } = req.body;
    console.log(id, commentid, comment);
    const postFound = posts.find(p => (p.id === id));
    const commentFound = postFound.comments.find(c => (c.id === commentid));
    console.log(commentFound); 
    commentFound.comment = comment;
    res.redirect(`/posts/${id}/comments`);
})
app.delete("/posts/:id/comments/:commentid", (req, res) => {
    const { id, commentid } = req.params;
    const postFound = posts.find(p => (p.id === id));
    const commentFound = postFound.comments.filter(c => (c.id !== commentid));
    postFound.comments = commentFound;
    console.log(posts);
    res.redirect(`/posts/${id}`);
    
})
app.delete("/posts/:id/comments/:commentid/details", (req, res) => {
    const { id, commentid } = req.params;
    const postFound = posts.find(p => (p.id === id));
    const commentFound = postFound.comments.filter(c => (c.id !== commentid));
    postFound.comments = commentFound;
    console.log(posts);
    res.redirect(`/posts/${id}/comments`);

})

//COMMENT SECTION
app.get("/", (req, res) => {
    // res.send("Hello, Welcome to My First Website, How can I help you today?");
    res.render("home/homePage.ejs")
    
})
app.get("/comments", (req, res) => {
    console.log("all comments")
    res.render("comments/index.ejs",{comments:comments})
})
app.get("/comments/new", (req, res) => {
    console.log("New Page");
    res.render("comments/new.ejs");
})
app.post("/comments", (req, res) => {
    const { username, comment } = req.body;
    const newObj = {
        id: uuid(),
        username: username,
        comment: comment

    }
    comments.push(newObj);
    res.redirect("/comments");
})
app.get("/comments/:id", (req, res) => {
    console.log("Specific comment")
    const { id } = req.params;
    const commentFound = comments.find(c => (c.id === id));
    res.render("comments/detailViews.ejs", { comments: commentFound });
})
app.get("/comments/:id/edit", (req, res) => {
    const { id } = req.params;
    const comment = comments.find(c => (c.id === id));
    res.render("comments/edit.ejs", { comment: comment });

})
app.patch("/comments/:id", (req, res) => {
    console.log("Patching comment")
    const { id } = req.params;
    
    const { comment } = req.body;
    const commentFound = comments.find(c => (c.id === id));
    commentFound.comment = comment;
    res.redirect("/comments");
    // res.send("Update successful")
})
app.delete("/comments/:id", (req, res) => {
    console.log("Deleting comment");

    const { id } = req.params;

    comments = comments.filter(c => c.id !== id);
    console.log("AFTER comment")
    res.redirect("/comments");
})

app.listen(3000, () => {
    console.log("Listening on port 3000")
})