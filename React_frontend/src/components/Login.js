import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import {
  Container,
  Paper,
  TextField,
  Button,
  Typography,
  Box,
  Alert,
} from '@mui/material';
import { useForm } from 'react-hook-form';

const Login = () => {
  const navigate = useNavigate();
  const [error, setError] = useState('');
  const { register, handleSubmit, formState: { errors } } = useForm();

  const onSubmit = (data) => {
    // Simple authentication - in a real app, this would validate against a backend
    if (data.användarNamn === 'you' && data.lösenord === 'password') {
      // Store user session
      localStorage.setItem('isLoggedIn', 'true');
      localStorage.setItem('currentUser', data.användarNamn);
      navigate('/start');
    } else {
      setError('Felaktigt användarnamn eller lösenord');
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
            >
              Logga in
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
