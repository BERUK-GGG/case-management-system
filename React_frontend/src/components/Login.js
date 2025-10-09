import React, { useState, useEffect } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import {
  Container,
  Paper,
  TextField,
  Button,
  Typography,
  Box,
  Alert,
  CircularProgress,
} from '@mui/material';
import { useForm } from 'react-hook-form';
import { useAuth } from '../contexts/AuthContext';


const Login = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const { login, isAuthenticated } = useAuth();
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const { register, handleSubmit, formState: { errors } } = useForm();

  // Redirect if already authenticated
  useEffect(() => {
    if (isAuthenticated) {
      const from = location.state?.from?.pathname || '/start';
      navigate(from, { replace: true });
    }
  }, [isAuthenticated, navigate, location]);

  const onSubmit = async (data) => {
    setLoading(true);
    setError('');
    
    try {
      await login(data.användarNamn, data.lösenord);
      // Navigation will be handled by useEffect above
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <Container component="main" maxWidth="xs">
      <Box
        sx={{
          marginTop: 8,
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
        }}
      >
        <Paper elevation={3} sx={{ padding: 4, width: '100%' }}>
          <Typography component="h1" variant="h4" align="center" gutterBottom>
            RB Ärendesystem
          </Typography>
          <Typography component="h2" variant="h6" align="center" color="textSecondary" gutterBottom>
            Logga in
          </Typography>
          
          {error && (
            <Alert severity="error" sx={{ mb: 2 }}>
              {error}
            </Alert>
          )}
          
          <Box component="form" onSubmit={handleSubmit(onSubmit)} sx={{ mt: 1 }}>
            <TextField
              margin="normal"
              required
              fullWidth
              id="användarNamn"
              label="Användarnamn"
              name="användarNamn"
              autoComplete="username"
              autoFocus
              {...register('användarNamn', { required: 'Användarnamn krävs' })}
              error={!!errors.användarNamn}
              helperText={errors.användarNamn?.message}
            />
            <TextField
              margin="normal"
              required
              fullWidth
              name="lösenord"
              label="Lösenord"
              type="password"
              id="lösenord"
              autoComplete="current-password"
              {...register('lösenord', { required: 'Lösenord krävs' })}
              error={!!errors.lösenord}
              helperText={errors.lösenord?.message}
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
              disabled={loading}
            >
              {loading ? <CircularProgress size={24} /> : 'Logga in'}
            </Button>
          </Box>
          
          <Typography variant="body2" color="textSecondary" align="center" sx={{ mt: 2 }}>
            Test användarnamn: <strong>you</strong><br />
            Test lösenord: <strong>password</strong>
          </Typography>
        </Paper>
      </Box>
      
    </Container>
    
  );
};

export default Login;
