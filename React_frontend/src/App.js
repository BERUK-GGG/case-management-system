import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import { AuthProvider } from './contexts/AuthContext';
import ProtectedRoute from './components/ProtectedRoute';
import Login from './components/Login';
import StartSida from './components/StartSida';
import NyKund from './components/NyKund';
import BokaTid from './components/BokaTid';
import RegistreraJournal from './components/RegistreraJournal';
import BeställReservdel from './components/BeställReservdel';
import UppdateraKund from './components/UppdateraKund';
import UppdateraBokning from './components/UppdateraBokning';

const theme = createTheme({
  palette: {
    primary: {
      main: '#1976d2',
    },
    secondary: {
      main: '#dc004e',
    },
  },
});

function App() {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <AuthProvider>
        <Router>
          <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/login" element={<Login />} />
            <Route path="/start" element={
              <ProtectedRoute>
                <StartSida />
              </ProtectedRoute>
            } />
            <Route path="/ny-kund" element={
              <ProtectedRoute>
                <NyKund />
              </ProtectedRoute>
            } />
            <Route path="/boka-tid" element={
              <ProtectedRoute>
                <BokaTid />
              </ProtectedRoute>
            } />
            <Route path="/registrera-journal" element={
              <ProtectedRoute>
                <RegistreraJournal />
              </ProtectedRoute>
            } />
            <Route path="/bestall-reservdel" element={
              <ProtectedRoute>
                <BeställReservdel />
              </ProtectedRoute>
            } />
            <Route path="/uppdatera-kund" element={
              <ProtectedRoute>
                <UppdateraKund />
              </ProtectedRoute>
            } />
            <Route path="/uppdatera-bokning" element={
              <ProtectedRoute>
                <UppdateraBokning />
              </ProtectedRoute>
            } />
          </Routes>
        </Router>
      </AuthProvider>
    </ThemeProvider>
  );
}

export default App;
