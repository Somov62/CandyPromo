import React from 'react';
import {BrowserRouter, Route, Routes} from "react-router-dom";
import App from "@/App/App.jsx";
import Main from "@/App/Pages/Main/Main.jsx";
import Profile from "@/App/Pages/Profile/Profile.jsx";
import Admin from "@/App/Pages/Admin/Admin.jsx";
import NotFound from "@/App/Pages/NotFound/NotFound.jsx";

const AppRouter = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route
                    path="/"
                    element={<App/>}>
                    <Route
                        path=""
                        element={<Main/>}/>

                    <Route
                        path="profile"
                        element={<Profile/>}/>

                    <Route
                        path="admin"
                        element={<Admin/>}/>
                </Route>
                <Route
                    path="*"
                    element={<NotFound/>}/>
            </Routes>
        </BrowserRouter>
    );
};

export default AppRouter;