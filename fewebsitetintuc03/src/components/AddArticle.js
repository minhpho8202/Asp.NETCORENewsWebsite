import React, { Component, useContext, useEffect, useState } from 'react';
import { CKEditor } from '@ckeditor/ckeditor5-react';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { Container } from 'react-bootstrap';
import axios from 'axios';
import { MyUserContext } from '../App';
import { useNavigate, useParams } from 'react-router-dom';

const AddArticle = () => {
    const [user, dispatch] = useContext(MyUserContext);
    const { id } = useParams();
    const [title, setTitle] = useState('');
    const [content, setContent] = useState('');
    const [article, setArticle] = useState([]);
    const nav = useNavigate();

    const loadArticle = async () => {
        try {
            const response = await axios.get(`https://localhost:7019/api/Article/get-article-by-id/${id}`);
            setArticle(response.data.data);
            setTitle(response.data.data.title);
            setContent(response.data.data.content);
            console.log(response.data.data);
        } catch (error) {
            console.error('failed', error);
        }
    }
    console.log(id);

    useEffect(() => {
        if(id !== undefined) {
            loadArticle();
        }
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (title === '' && content === '') {
            alert("Hãy nhập nội dung")
        } else {
            const res = await axios.post("https://localhost:7019/api/Article/create-article", {
                "id": 0,
                "userId": user.id,
                "title": title,
                "content": content
            })
            if(res.status === 200){
                nav("/");
            }
            else {
                alert("Thêm thất bại, đang có lỗi xảy ra");
            }
        }
        console.log('Title:', title);
        console.log('Content:', content);
    };

    const handleUpdateAricle = async (e) => {
        e.preventDefault();
        if (title === '' && content === '') {
            alert("Hãy nhập nội dung")
        } else {
            const res = await axios.post("https://localhost:7019/api/Article/update-article", {
                "id": id,
                "userId": user.id,
                "title": title,
                "content": content
            })
            if(res.status === 200){
                nav("/");
            }
            else {
                alert("Thêm thất bại, đang có lỗi xảy ra");
            }
        }
        console.log('Title:', title);
        console.log('Content:', content);
    }

    return (
        <Container>
            {
                id === undefined ? <>
                <h1 className="text-center text-success">Thêm bài viết</h1>
                <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label htmlFor="title">Tiêu đề bài viết</label>
                    <input
                        type="text"
                        className="form-control"
                        id="title"
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                        required
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="content">Nội dung bài viết</label>
                    <CKEditor
                        editor={ClassicEditor}
                        data={content}
                        onChange={(event, editor) => setContent(editor.getData())}
                        required
                    />
                </div>
                <button type="submit" className="btn btn-primary mt-2 mb-2">
                    Lưu bài viết
                </button>
            </form>
                </>:
                <>
                <h1 className="text-center text-success">Sửa bài viết</h1>
                <form onSubmit={handleUpdateAricle}>
                <div className="form-group">
                    <label htmlFor="title">Tiêu đề bài viết</label>
                    <input
                        type="text"
                        className="form-control"
                        id="title"
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                        required
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="content">Nội dung bài viết</label>
                    <CKEditor
                        editor={ClassicEditor}
                        data={content}
                        onChange={(event, editor) => setContent(editor.getData())}
                        required
                    />
                </div>
                <button type="submit" className="btn btn-primary mt-2 mb-2">
                    Lưu bài viết
                </button>
            </form>
                </>
            }
            
        </Container>
    )
}

export default AddArticle;