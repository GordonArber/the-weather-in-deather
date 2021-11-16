using System;
using System.Collections.Generic;
using Xunit.Sdk;

public class WeatherStation
{
    Reading _reading;
    readonly List<DateTime> _recordDates = new();
    readonly List<decimal> _temperatures = new();

    public void AcceptReading(Reading reading)
    {
        _reading = reading;
        _recordDates.Add(DateTime.Now);
        _temperatures.Add(reading.Temperature);
    }

    public void ClearAll()
    {
        _reading = new Reading();
        _recordDates.Clear();
        _temperatures.Clear();
    }

    public decimal LatestTemperature => _reading.Temperature;

    public decimal LatestPressure => _reading.Pressure;

    public decimal LatestRainfall => _reading.Rainfall;

    public bool HasHistory => _recordDates.Count > 1;
    public Outlook ShortTermOutlook
    {
        get
        {
            if (_reading.Equals(new Reading()))
            {
                throw new ArgumentException();
            }
            else
            {
                if (_reading.Pressure < 10m && _reading.Temperature < 30m)
                {
                    return Outlook.Cool;
                }
                else if (_reading.Temperature > 50)
                {
                    return Outlook.Good;
                }
                else
                {
                    return Outlook.Warm;
                }
            }
        }
    }

    public Outlook LongTermOutlook
    {
        get
        {
            return _reading.WindDirection switch
            {
                WindDirection.Southerly => Outlook.Good,
                WindDirection.Easterly when _reading.Temperature > 20 => Outlook.Good,
                WindDirection.Northerly => Outlook.Cool,
                WindDirection.Easterly when _reading.Temperature <= 20 => Outlook.Warm,
                WindDirection.Westerly => Outlook.Rainy,
                _ => throw new ArgumentException()
            };
        }
    }

    public State RunSelfTest() => _reading.Equals(new Reading()) ? State.Bad : State.Good;
}

/*** Please do not modify this struct ***/
public struct Reading
{
    public decimal Temperature { get; }
    public decimal Pressure { get; }
    public decimal Rainfall { get; }
    public WindDirection WindDirection { get; }

    public Reading(decimal temperature, decimal pressure,
        decimal rainfall, WindDirection windDirection)
    {
        Temperature = temperature;
        Pressure = pressure;
        Rainfall = rainfall;
        WindDirection = windDirection;
    }
}

/*** Please do not modify this enum ***/
public enum State
{
    Good,
    Bad
}

/*** Please do not modify this enum ***/
public enum Outlook
{
    Cool,
    Rainy,
    Warm,
    Good
}

/*** Please do not modify this enum ***/
public enum WindDirection
{
    Unknown = 0,    // default
    Northerly,
    Easterly,
    Southerly,
    Westerly
}
