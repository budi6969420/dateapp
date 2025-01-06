import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import HomePage from "../../pages/HomePage/HomePage";
import LoginPage from "../../pages/Login/LoginPage";
import ProtectedRoute from "../ProtectedRoute/Index";
import UsersPage from "../../pages/UsersPage/UsersPage";
import UserPage from "../../pages/UserPage/UserPage";
import NotFoundPage from "../../pages/NotFoundPage/Index";
import DatesPage from "../../pages/DatesPage/Index";

const AppRoutes = () => (
  <Router>
    <Routes>
      <Route
        path="*"
        element={
          <ProtectedRoute>
            <NotFoundPage />
          </ProtectedRoute>
        }
      />
      <Route path="/login" element={<LoginPage />} />
      <Route
        path="/"
        element={
          <ProtectedRoute>
            <HomePage />
          </ProtectedRoute>
        }
      />
      <Route
        path="/user/:id"
        element={
          <ProtectedRoute>
            <UserPage />
          </ProtectedRoute>
        }
      />
      <Route
        path="/users"
        element={
          <ProtectedRoute>
            <UsersPage />
          </ProtectedRoute>
        }
      />
      <Route
        path="/dates"
        element={
          <ProtectedRoute>
            <DatesPage />
          </ProtectedRoute>
        }
      />
    </Routes>
  </Router>
);

export default AppRoutes;
