import React, { useEffect, useState } from 'react';
import { Card, Container, Row, Col, Button, Form, Pagination } from 'react-bootstrap';
import Axios from 'axios';
import { Link } from 'react-router-dom';
import MySpinner from '../layout/MySpinner';
import axios from 'axios';

const Home = () => {
  const [articles, setArticles] = useState([]);
  const [keyword, setKeyword] = useState('');
  const [loading, setLoading] = useState(false);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);

  const pageSize = 4;

  const loadArticles = async () => {
    try {
      const count = await axios.get("https://localhost:7019/api/Article/count-all-article");
      setTotalPages(Math.ceil(count.data / pageSize));
      const response = await Axios.post('https://localhost:7019/api/Article/search-article', {
        keyword: keyword,
        page: currentPage,
        size: pageSize,
      });
      setArticles(response.data.data.data);
      setKeyword('')
      console.log(response.data.data.data);
    } catch (error) {
      console.error('failed', error);
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    loadArticles();
  };

  useEffect(() => {
    loadArticles();
  }, [currentPage])

  return (
    <Container>
      <Form onSubmit={handleSubmit} className="mb-3 mt-2">
        <Form.Group>
          <Form.Control
            type="text"
            placeholder="Tìm kiếm bài viết"
            value={keyword}
            onChange={(e) => { setKeyword(e.target.value); }}
          />
        </Form.Group>
        <Button variant="primary" type="submit" className='mt-2'>
          Tìm kiếm
        </Button>
      </Form>
      {loading ? (
        <MySpinner />
      ) : (
        <>
          <Row className="mt-2 mb-2">
            {articles.map((article) => (
              <Col key={article.id} md={3} className="mt-2 mb-2">
                <Card style={{ width: '18rem' }}>
                  <Card.Img variant="top" src="/logo512.png" />
                  <Card.Body>
                    <Card.Title>{article.title}</Card.Title>
                    <Link variant="primary" className='btn btn-success' to={`/articles/${article.id}`}>Xem chi tiết</Link>
                  </Card.Body>
                </Card>
              </Col>
            ))}
          </Row>
          <Pagination className="justify-content-center">
            {Array.from({ length: totalPages }, (_, index) => (
              <Pagination.Item
                key={index}
                active={index + 1 === currentPage}
                onClick={() => setCurrentPage(index + 1)}
              >
                {index + 1}
              </Pagination.Item>
            ))}
          </Pagination>
        </>
      )}
    </Container>
  );
}

export default Home;