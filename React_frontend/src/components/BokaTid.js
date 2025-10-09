import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import {
  Container,
  Paper,
  TextField,
  Button,
  Typography,
  Box,
  Alert,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
} from '@mui/material';
import Header from './Header';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { useForm, Controller } from 'react-hook-form';
import dayjs from 'dayjs';
import 'dayjs/locale/sv';
import { customerService, visitService, mechanicService } from '../services/api';
import { validationRules, sanitizeInput } from '../utils/validation';

// Set Swedish locale for dayjs
dayjs.locale('sv');

const BokaTid = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [success, setSuccess] = useState(false);
  const [error, setError] = useState('');
  const [customers, setCustomers] = useState([]);
  const [mechanics, setMechanics] = useState([]);
  
  const { register, handleSubmit, control, formState: { errors }, reset } = useForm({
    defaultValues: {
      dateAndTime: dayjs(),
    }
  });

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [customersResponse, mechanicsResponse] = await Promise.all([
          customerService.getAll(),
          mechanicService.getAll()
        ]);
        setCustomers(customersResponse.data);
        setMechanics(mechanicsResponse.data);
      } catch (err) {
        console.error('Fel vid hämtning av data:', err);
        setError('Kunde inte hämta kunddata och mekanikerdata.');
      }
    };

    fetchData();
  }, []);

  const onSubmit = async (data) => {
    setLoading(true);
    setError('');
    
    try {
      // Sanitize input data
      const visitData = {
        kundId: parseInt(data.kundId),
        dateAndTime: data.dateAndTime.toISOString(),
        syfte: sanitizeInput.text(data.syfte),
        mekanikerId: parseInt(data.mekanikerId),
      };
      
      const response = await visitService.create(visitData);
      console.log('Besök skapat:', response.data);
      setSuccess(true);
      reset({
        dateAndTime: dayjs(),
        kundId: '',
        mekanikerId: '',
        syfte: '',
      });
      
      setTimeout(() => setSuccess(false), 3000);
    } catch (err) {
      console.error('Fel vid skapande av besök:', err);
      setError('Kunde inte boka tiden. Försök igen.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="app-container">
      <Header />

      <Container maxWidth="md" sx={{ mt: 4, mb: 4 }}>
        <Paper elevation={3} sx={{ p: 4 }}>
          <Typography variant="h4" gutterBottom align="center">
            Boka Nytt Besök
          </Typography>
          
          {success && (
            <Alert severity="success" sx={{ mb: 2 }}>
              Besöket har bokats framgångsrikt!
            </Alert>
          )}
          
          {error && (
            <Alert severity="error" sx={{ mb: 2 }}>
              {error}
            </Alert>
          )}

          <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="sv">
            <Box component="form" onSubmit={handleSubmit(onSubmit)} sx={{ mt: 2 }}>
              <FormControl fullWidth margin="normal" required>
                <InputLabel id="kund-label">Kund</InputLabel>
                <Controller
                  name="kundId"
                  control={control}
                  rules={{ required: 'Kund krävs' }}
                  render={({ field }) => (
                    <Select
                      {...field}
                      labelId="kund-label"
                      label="Kund"
                      error={!!errors.kundId}
                    >
                      {customers.map((customer) => (
                        <MenuItem key={customer.id} value={customer.id}>
                          {customer.namn} - {customer.personNr}
                        </MenuItem>
                      ))}
                    </Select>
                  )}
                />
                {errors.kundId && (
                  <Typography variant="caption" color="error">
                    {errors.kundId.message}
                  </Typography>
                )}
              </FormControl>

              <Controller
                name="dateAndTime"
                control={control}
                rules={{ required: 'Datum och tid krävs' }}
                render={({ field }) => (
                  <DateTimePicker
                    {...field}
                    label="Datum och tid"
                    renderInput={(params) => (
                      <TextField
                        {...params}
                        fullWidth
                        margin="normal"
                        required
                        error={!!errors.dateAndTime}
                        helperText={errors.dateAndTime?.message}
                      />
                    )}
                    minDateTime={dayjs()}
                  />
                )}
              />

              <TextField
                margin="normal"
                required
                fullWidth
                id="syfte"
                label="Syfte med besöket"
                name="syfte"
                multiline
                rows={3}
                {...register('syfte', {
                  ...validationRules.description,
                  required: 'Syfte krävs',
                  minLength: { value: 3, message: 'Syfte måste vara minst 3 tecken' }
                })}
                error={!!errors.syfte}
                helperText={errors.syfte?.message}
              />

              <FormControl fullWidth margin="normal" required>
                <InputLabel id="mekaniker-label">Mekaniker</InputLabel>
                <Controller
                  name="mekanikerId"
                  control={control}
                  rules={{ required: 'Mekaniker krävs' }}
                  render={({ field }) => (
                    <Select
                      {...field}
                      labelId="mekaniker-label"
                      label="Mekaniker"
                      error={!!errors.mekanikerId}
                    >
                      {mechanics.map((mechanic) => (
                        <MenuItem key={mechanic.id} value={mechanic.id}>
                          {mechanic.namn} - {mechanic.specialisering}
                        </MenuItem>
                      ))}
                    </Select>
                  )}
                />
                {errors.mekanikerId && (
                  <Typography variant="caption" color="error">
                    {errors.mekanikerId.message}
                  </Typography>
                )}
              </FormControl>

              <Box sx={{ mt: 3, display: 'flex', gap: 2 }}>
                <Button
                  type="submit"
                  variant="contained"
                  disabled={loading}
                  sx={{ flex: 1 }}
                >
                  {loading ? 'Bokar...' : 'Boka Tid'}
                </Button>
                <Button
                  variant="outlined"
                  onClick={() => reset({ dateAndTime: dayjs() })}
                  disabled={loading}
                  sx={{ flex: 1 }}
                >
                  Rensa
                </Button>
              </Box>
            </Box>
          </LocalizationProvider>
        </Paper>
      </Container>
    </div>
  );
};

export default BokaTid;
