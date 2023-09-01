import { BrowserRouter, Route, Routes } from "react-router-dom";
import Header from "./layout/Header";
import Footer from "./layout/Footer";
import Home from "./components/Home";
import 'bootstrap/dist/css/bootstrap.min.css';
import DetailArticle from "./components/DetailArticle";
import Login from "./components/Login";
import { createContext, useReducer } from "react";
import cookie from "react-cookies";
import MyUserReducer from "./reducers/MyUserReducer";

export const MyUserContext = createContext();

const App = () => {
  const u = cookie.load("user");
  let init;
  if (u !== null) {
    init = cookie.load("user");
  }
  const [user, dispatch] = useReducer(MyUserReducer, init || null);

  return (
    <MyUserContext.Provider value={[user, dispatch]}>
    <BrowserRouter>
      <Header/>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="articles/:id" element={<DetailArticle />} />
        <Route path="/login" element={<Login />} />
      </Routes>
      <Footer/>
    </BrowserRouter>
    </MyUserContext.Provider>
  )
}

export default App;