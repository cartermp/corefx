// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// ------------------------------------------------------------------------------
// Changes to this file must follow the http://aka.ms/api-review process.
// ------------------------------------------------------------------------------


namespace System.Globalization
{
    /// <summary>
    /// Represents time in divisions, such as weeks, months, and years.
    /// </summary>
    public abstract partial class Calendar
    {
        /// <summary>
        /// Represents the current era of the current calendar.
        /// </summary>
        public const int CurrentEra = 0;
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Globalization.Calendar" /> class.
        /// </summary>
        protected Calendar() { }
        /// <summary>
        /// When overridden in a derived class, gets the list of eras in the current calendar.
        /// </summary>
        /// <returns>
        /// An array of integers that represents the eras in the current calendar.
        /// </returns>
        public abstract int[] Eras { get; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:System.Globalization.Calendar" /> object
        /// is read-only.
        /// </summary>
        /// <returns>
        /// true if this <see cref="T:System.Globalization.Calendar" /> object is read-only; otherwise,
        /// false.
        /// </returns>
        public bool IsReadOnly { get { return default(bool); } }
        /// <summary>
        /// Gets the latest date and time supported by this <see cref="T:System.Globalization.Calendar" />
        /// object.
        /// </summary>
        /// <returns>
        /// The latest date and time supported by this calendar. The default is <see cref="F:System.DateTime.MaxValue" />.
        /// </returns>
        public virtual System.DateTime MaxSupportedDateTime { get { return default(System.DateTime); } }
        /// <summary>
        /// Gets the earliest date and time supported by this <see cref="T:System.Globalization.Calendar" />
        /// object.
        /// </summary>
        /// <returns>
        /// The earliest date and time supported by this calendar. The default is <see cref="F:System.DateTime.MinValue" />.
        /// </returns>
        public virtual System.DateTime MinSupportedDateTime { get { return default(System.DateTime); } }
        /// <summary>
        /// Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.
        /// </summary>
        /// <returns>
        /// The last year of a 100-year range that can be represented by a 2-digit year.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">
        /// The current <see cref="T:System.Globalization.Calendar" /> object is read-only.
        /// </exception>
        public virtual int TwoDigitYearMax { get { return default(int); } set { } }
        /// <summary>
        /// Returns a <see cref="T:System.DateTime" /> that is the specified number of days away from
        /// the specified <see cref="T:System.DateTime" />.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.DateTime" /> that results from adding the specified number of days
        /// to the specified <see cref="T:System.DateTime" />.
        /// </returns>
        /// <param name="time">The <see cref="T:System.DateTime" /> to which to add days.</param>
        /// <param name="days">The number of days to add.</param>
        /// <exception cref="T:System.ArgumentException">
        /// The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="days" /> is outside the supported range of the <see cref="T:System.DateTime" />
        /// return value.
        /// </exception>
        public virtual System.DateTime AddDays(System.DateTime time, int days) { return default(System.DateTime); }
        /// <summary>
        /// Returns a <see cref="T:System.DateTime" /> that is the specified number of hours away from
        /// the specified <see cref="T:System.DateTime" />.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.DateTime" /> that results from adding the specified number of hours
        /// to the specified <see cref="T:System.DateTime" />.
        /// </returns>
        /// <param name="time">The <see cref="T:System.DateTime" /> to which to add hours.</param>
        /// <param name="hours">The number of hours to add.</param>
        /// <exception cref="T:System.ArgumentException">
        /// The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="hours" /> is outside the supported range of the <see cref="T:System.DateTime" />
        /// return value.
        /// </exception>
        public virtual System.DateTime AddHours(System.DateTime time, int hours) { return default(System.DateTime); }
        /// <summary>
        /// Returns a <see cref="T:System.DateTime" /> that is the specified number of milliseconds away
        /// from the specified <see cref="T:System.DateTime" />.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.DateTime" /> that results from adding the specified number of milliseconds
        /// to the specified <see cref="T:System.DateTime" />.
        /// </returns>
        /// <param name="time">The <see cref="T:System.DateTime" /> to add milliseconds to.</param>
        /// <param name="milliseconds">The number of milliseconds to add.</param>
        /// <exception cref="T:System.ArgumentException">
        /// The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="milliseconds" /> is outside the supported range of the <see cref="T:System.DateTime" />
        /// return value.
        /// </exception>
        public virtual System.DateTime AddMilliseconds(System.DateTime time, double milliseconds) { return default(System.DateTime); }
        /// <summary>
        /// Returns a <see cref="T:System.DateTime" /> that is the specified number of minutes away from
        /// the specified <see cref="T:System.DateTime" />.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.DateTime" /> that results from adding the specified number of minutes
        /// to the specified <see cref="T:System.DateTime" />.
        /// </returns>
        /// <param name="time">The <see cref="T:System.DateTime" /> to which to add minutes.</param>
        /// <param name="minutes">The number of minutes to add.</param>
        /// <exception cref="T:System.ArgumentException">
        /// The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="minutes" /> is outside the supported range of the <see cref="T:System.DateTime" />
        /// return value.
        /// </exception>
        public virtual System.DateTime AddMinutes(System.DateTime time, int minutes) { return default(System.DateTime); }
        /// <summary>
        /// When overridden in a derived class, returns a <see cref="T:System.DateTime" /> that is the
        /// specified number of months away from the specified <see cref="T:System.DateTime" />.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.DateTime" /> that results from adding the specified number of months
        /// to the specified <see cref="T:System.DateTime" />.
        /// </returns>
        /// <param name="time">The <see cref="T:System.DateTime" /> to which to add months.</param>
        /// <param name="months">The number of months to add.</param>
        /// <exception cref="T:System.ArgumentException">
        /// The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="months" /> is outside the supported range of the <see cref="T:System.DateTime" />
        /// return value.
        /// </exception>
        public abstract System.DateTime AddMonths(System.DateTime time, int months);
        /// <summary>
        /// Returns a <see cref="T:System.DateTime" /> that is the specified number of seconds away from
        /// the specified <see cref="T:System.DateTime" />.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.DateTime" /> that results from adding the specified number of seconds
        /// to the specified <see cref="T:System.DateTime" />.
        /// </returns>
        /// <param name="time">The <see cref="T:System.DateTime" /> to which to add seconds.</param>
        /// <param name="seconds">The number of seconds to add.</param>
        /// <exception cref="T:System.ArgumentException">
        /// The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="seconds" /> is outside the supported range of the <see cref="T:System.DateTime" />
        /// return value.
        /// </exception>
        public virtual System.DateTime AddSeconds(System.DateTime time, int seconds) { return default(System.DateTime); }
        /// <summary>
        /// Returns a <see cref="T:System.DateTime" /> that is the specified number of weeks away from
        /// the specified <see cref="T:System.DateTime" />.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.DateTime" /> that results from adding the specified number of weeks
        /// to the specified <see cref="T:System.DateTime" />.
        /// </returns>
        /// <param name="time">The <see cref="T:System.DateTime" /> to which to add weeks.</param>
        /// <param name="weeks">The number of weeks to add.</param>
        /// <exception cref="T:System.ArgumentException">
        /// The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="weeks" /> is outside the supported range of the <see cref="T:System.DateTime" />
        /// return value.
        /// </exception>
        public virtual System.DateTime AddWeeks(System.DateTime time, int weeks) { return default(System.DateTime); }
        /// <summary>
        /// When overridden in a derived class, returns a <see cref="T:System.DateTime" /> that is the
        /// specified number of years away from the specified <see cref="T:System.DateTime" />.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.DateTime" /> that results from adding the specified number of years
        /// to the specified <see cref="T:System.DateTime" />.
        /// </returns>
        /// <param name="time">The <see cref="T:System.DateTime" /> to which to add years.</param>
        /// <param name="years">The number of years to add.</param>
        /// <exception cref="T:System.ArgumentException">
        /// The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="years" /> is outside the supported range of the <see cref="T:System.DateTime" />
        /// return value.
        /// </exception>
        public abstract System.DateTime AddYears(System.DateTime time, int years);
        /// <summary>
        /// When overridden in a derived class, returns the day of the month in the specified
        /// <see cref="T:System.DateTime" />.
        /// </summary>
        /// <returns>
        /// A positive integer that represents the day of the month in the <paramref name="time" /> parameter.
        /// </returns>
        /// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
        public abstract int GetDayOfMonth(System.DateTime time);
        /// <summary>
        /// When overridden in a derived class, returns the day of the week in the specified
        /// <see cref="T:System.DateTime" />.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the <paramref name="time" />
        /// parameter.
        /// </returns>
        /// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
        public abstract System.DayOfWeek GetDayOfWeek(System.DateTime time);
        /// <summary>
        /// When overridden in a derived class, returns the day of the year in the specified
        /// <see cref="T:System.DateTime" />.
        /// </summary>
        /// <returns>
        /// A positive integer that represents the day of the year in the <paramref name="time" /> parameter.
        /// </returns>
        /// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
        public abstract int GetDayOfYear(System.DateTime time);
        /// <summary>
        /// Returns the number of days in the specified month and year of the current era.
        /// </summary>
        /// <returns>
        /// The number of days in the specified month in the specified year in the current era.
        /// </returns>
        /// <param name="year">An integer that represents the year.</param>
        /// <param name="month">A positive integer that represents the month.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" />
        /// is outside the range supported by the calendar.
        /// </exception>
        public virtual int GetDaysInMonth(int year, int month) { return default(int); }
        /// <summary>
        /// When overridden in a derived class, returns the number of days in the specified month, year,
        /// and era.
        /// </summary>
        /// <returns>
        /// The number of days in the specified month in the specified year in the specified era.
        /// </returns>
        /// <param name="year">An integer that represents the year.</param>
        /// <param name="month">A positive integer that represents the month.</param>
        /// <param name="era">An integer that represents the era.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" />
        /// is outside the range supported by the calendar.-or- <paramref name="era" /> is outside
        /// the range supported by the calendar.
        /// </exception>
        public abstract int GetDaysInMonth(int year, int month, int era);
        /// <summary>
        /// Returns the number of days in the specified year of the current era.
        /// </summary>
        /// <returns>
        /// The number of days in the specified year in the current era.
        /// </returns>
        /// <param name="year">An integer that represents the year.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="year" /> is outside the range supported by the calendar.
        /// </exception>
        public virtual int GetDaysInYear(int year) { return default(int); }
        /// <summary>
        /// When overridden in a derived class, returns the number of days in the specified year and era.
        /// </summary>
        /// <returns>
        /// The number of days in the specified year in the specified era.
        /// </returns>
        /// <param name="year">An integer that represents the year.</param>
        /// <param name="era">An integer that represents the era.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" />
        /// is outside the range supported by the calendar.
        /// </exception>
        public abstract int GetDaysInYear(int year, int era);
        /// <summary>
        /// When overridden in a derived class, returns the era in the specified <see cref="T:System.DateTime" />.
        /// </summary>
        /// <returns>
        /// An integer that represents the era in <paramref name="time" />.
        /// </returns>
        /// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
        public abstract int GetEra(System.DateTime time);
        /// <summary>
        /// Returns the hours value in the specified <see cref="T:System.DateTime" />.
        /// </summary>
        /// <returns>
        /// An integer from 0 to 23 that represents the hour in <paramref name="time" />.
        /// </returns>
        /// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
        public virtual int GetHour(System.DateTime time) { return default(int); }
        /// <summary>
        /// Calculates the leap month for a specified year and era.
        /// </summary>
        /// <returns>
        /// A positive integer that indicates the leap month in the specified year and era.-or-Zero if
        /// this calendar does not support a leap month or if the <paramref name="year" /> and <paramref name="era" />
        /// parameters do not specify a leap year.
        /// </returns>
        /// <param name="year">A year.</param>
        /// <param name="era">An era.</param>
        public virtual int GetLeapMonth(int year, int era) { return default(int); }
        /// <summary>
        /// Returns the milliseconds value in the specified <see cref="T:System.DateTime" />.
        /// </summary>
        /// <returns>
        /// A double-precision floating-point number from 0 to 999 that represents the milliseconds in
        /// the <paramref name="time" /> parameter.
        /// </returns>
        /// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
        public virtual double GetMilliseconds(System.DateTime time) { return default(double); }
        /// <summary>
        /// Returns the minutes value in the specified <see cref="T:System.DateTime" />.
        /// </summary>
        /// <returns>
        /// An integer from 0 to 59 that represents the minutes in <paramref name="time" />.
        /// </returns>
        /// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
        public virtual int GetMinute(System.DateTime time) { return default(int); }
        /// <summary>
        /// When overridden in a derived class, returns the month in the specified <see cref="T:System.DateTime" />.
        /// </summary>
        /// <returns>
        /// A positive integer that represents the month in <paramref name="time" />.
        /// </returns>
        /// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
        public abstract int GetMonth(System.DateTime time);
        /// <summary>
        /// Returns the number of months in the specified year in the current era.
        /// </summary>
        /// <returns>
        /// The number of months in the specified year in the current era.
        /// </returns>
        /// <param name="year">An integer that represents the year.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="year" /> is outside the range supported by the calendar.
        /// </exception>
        public virtual int GetMonthsInYear(int year) { return default(int); }
        /// <summary>
        /// When overridden in a derived class, returns the number of months in the specified year in the
        /// specified era.
        /// </summary>
        /// <returns>
        /// The number of months in the specified year in the specified era.
        /// </returns>
        /// <param name="year">An integer that represents the year.</param>
        /// <param name="era">An integer that represents the era.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" />
        /// is outside the range supported by the calendar.
        /// </exception>
        public abstract int GetMonthsInYear(int year, int era);
        /// <summary>
        /// Returns the seconds value in the specified <see cref="T:System.DateTime" />.
        /// </summary>
        /// <returns>
        /// An integer from 0 to 59 that represents the seconds in <paramref name="time" />.
        /// </returns>
        /// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
        public virtual int GetSecond(System.DateTime time) { return default(int); }
        /// <summary>
        /// Returns the week of the year that includes the date in the specified <see cref="T:System.DateTime" />
        /// value.
        /// </summary>
        /// <returns>
        /// A positive integer that represents the week of the year that includes the date in the <paramref name="time" />
        /// parameter.
        /// </returns>
        /// <param name="time">A date and time value.</param>
        /// <param name="rule">An enumeration value that defines a calendar week.</param>
        /// <param name="firstDayOfWeek">An enumeration value that represents the first day of the week.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="time" /> is earlier than <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" />
        /// or later than <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />.-or-
        /// <paramref name="firstDayOfWeek" /> is not a valid <see cref="T:System.DayOfWeek" /> value.-or-
        /// <paramref name="rule" /> is not a valid <see cref="T:System.Globalization.CalendarWeekRule" /> value.
        /// </exception>
        public virtual int GetWeekOfYear(System.DateTime time, System.Globalization.CalendarWeekRule rule, System.DayOfWeek firstDayOfWeek) { return default(int); }
        /// <summary>
        /// When overridden in a derived class, returns the year in the specified <see cref="T:System.DateTime" />.
        /// </summary>
        /// <returns>
        /// An integer that represents the year in <paramref name="time" />.
        /// </returns>
        /// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
        public abstract int GetYear(System.DateTime time);
        /// <summary>
        /// Determines whether the specified date in the current era is a leap day.
        /// </summary>
        /// <returns>
        /// true if the specified day is a leap day; otherwise, false.
        /// </returns>
        /// <param name="year">An integer that represents the year.</param>
        /// <param name="month">A positive integer that represents the month.</param>
        /// <param name="day">A positive integer that represents the day.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" />
        /// is outside the range supported by the calendar.-or- <paramref name="day" /> is outside
        /// the range supported by the calendar.
        /// </exception>
        public virtual bool IsLeapDay(int year, int month, int day) { return default(bool); }
        /// <summary>
        /// When overridden in a derived class, determines whether the specified date in the specified
        /// era is a leap day.
        /// </summary>
        /// <returns>
        /// true if the specified day is a leap day; otherwise, false.
        /// </returns>
        /// <param name="year">An integer that represents the year.</param>
        /// <param name="month">A positive integer that represents the month.</param>
        /// <param name="day">A positive integer that represents the day.</param>
        /// <param name="era">An integer that represents the era.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" />
        /// is outside the range supported by the calendar.-or- <paramref name="day" /> is outside
        /// the range supported by the calendar.-or- <paramref name="era" /> is outside the range supported
        /// by the calendar.
        /// </exception>
        public abstract bool IsLeapDay(int year, int month, int day, int era);
        /// <summary>
        /// Determines whether the specified month in the specified year in the current era is a leap month.
        /// </summary>
        /// <returns>
        /// true if the specified month is a leap month; otherwise, false.
        /// </returns>
        /// <param name="year">An integer that represents the year.</param>
        /// <param name="month">A positive integer that represents the month.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" />
        /// is outside the range supported by the calendar.
        /// </exception>
        public virtual bool IsLeapMonth(int year, int month) { return default(bool); }
        /// <summary>
        /// When overridden in a derived class, determines whether the specified month in the specified
        /// year in the specified era is a leap month.
        /// </summary>
        /// <returns>
        /// true if the specified month is a leap month; otherwise, false.
        /// </returns>
        /// <param name="year">An integer that represents the year.</param>
        /// <param name="month">A positive integer that represents the month.</param>
        /// <param name="era">An integer that represents the era.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" />
        /// is outside the range supported by the calendar.-or- <paramref name="era" /> is outside
        /// the range supported by the calendar.
        /// </exception>
        public abstract bool IsLeapMonth(int year, int month, int era);
        /// <summary>
        /// Determines whether the specified year in the current era is a leap year.
        /// </summary>
        /// <returns>
        /// true if the specified year is a leap year; otherwise, false.
        /// </returns>
        /// <param name="year">An integer that represents the year.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="year" /> is outside the range supported by the calendar.
        /// </exception>
        public virtual bool IsLeapYear(int year) { return default(bool); }
        /// <summary>
        /// When overridden in a derived class, determines whether the specified year in the specified
        /// era is a leap year.
        /// </summary>
        /// <returns>
        /// true if the specified year is a leap year; otherwise, false.
        /// </returns>
        /// <param name="year">An integer that represents the year.</param>
        /// <param name="era">An integer that represents the era.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="era" />
        /// is outside the range supported by the calendar.
        /// </exception>
        public abstract bool IsLeapYear(int year, int era);
        /// <summary>
        /// Returns a <see cref="T:System.DateTime" /> that is set to the specified date and time in the
        /// current era.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.DateTime" /> that is set to the specified date and time in the current
        /// era.
        /// </returns>
        /// <param name="year">An integer that represents the year.</param>
        /// <param name="month">A positive integer that represents the month.</param>
        /// <param name="day">A positive integer that represents the day.</param>
        /// <param name="hour">An integer from 0 to 23 that represents the hour.</param>
        /// <param name="minute">An integer from 0 to 59 that represents the minute.</param>
        /// <param name="second">An integer from 0 to 59 that represents the second.</param>
        /// <param name="millisecond">An integer from 0 to 999 that represents the millisecond.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" />
        /// is outside the range supported by the calendar.-or- <paramref name="day" /> is outside
        /// the range supported by the calendar.-or- <paramref name="hour" /> is less than zero or greater
        /// than 23.-or- <paramref name="minute" /> is less than zero or greater than 59.-or- <paramref name="second" />
        /// is less than zero or greater than 59.-or- <paramref name="millisecond" />
        /// is less than zero or greater than 999.
        /// </exception>
        public virtual System.DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond) { return default(System.DateTime); }
        /// <summary>
        /// When overridden in a derived class, returns a <see cref="T:System.DateTime" /> that is set
        /// to the specified date and time in the specified era.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.DateTime" /> that is set to the specified date and time in the current
        /// era.
        /// </returns>
        /// <param name="year">An integer that represents the year.</param>
        /// <param name="month">A positive integer that represents the month.</param>
        /// <param name="day">A positive integer that represents the day.</param>
        /// <param name="hour">An integer from 0 to 23 that represents the hour.</param>
        /// <param name="minute">An integer from 0 to 59 that represents the minute.</param>
        /// <param name="second">An integer from 0 to 59 that represents the second.</param>
        /// <param name="millisecond">An integer from 0 to 999 that represents the millisecond.</param>
        /// <param name="era">An integer that represents the era.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="year" /> is outside the range supported by the calendar.-or- <paramref name="month" />
        /// is outside the range supported by the calendar.-or- <paramref name="day" /> is outside
        /// the range supported by the calendar.-or- <paramref name="hour" /> is less than zero or greater
        /// than 23.-or- <paramref name="minute" /> is less than zero or greater than 59.-or- <paramref name="second" />
        /// is less than zero or greater than 59.-or- <paramref name="millisecond" />
        /// is less than zero or greater than 999.-or- <paramref name="era" /> is outside the range supported
        /// by the calendar.
        /// </exception>
        public abstract System.DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era);
        /// <summary>
        /// Converts the specified year to a four-digit year by using the
        /// <see cref="P:System.Globalization.Calendar.TwoDigitYearMax" /> property to determine the appropriate century.
        /// </summary>
        /// <returns>
        /// An integer that contains the four-digit representation of <paramref name="year" />.
        /// </returns>
        /// <param name="year">A two-digit or four-digit integer that represents the year to convert.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="year" /> is outside the range supported by the calendar.
        /// </exception>
        public virtual int ToFourDigitYear(int year) { return default(int); }
    }
    /// <summary>
    /// Defines different rules for determining the first week of the year.
    /// </summary>
    public enum CalendarWeekRule
    {
        /// <summary>
        /// Indicates that the first week of the year starts on the first day of the year and ends before
        /// the following designated first day of the week. The value is 0.
        /// </summary>
        FirstDay = 0,
        /// <summary>
        /// Indicates that the first week of the year is the first week with four or more days before the
        /// designated first day of the week. The value is 2.
        /// </summary>
        FirstFourDayWeek = 2,
        /// <summary>
        /// Indicates that the first week of the year begins on the first occurrence of the designated
        /// first day of the week on or after the first day of the year. The value is 1.
        /// </summary>
        FirstFullWeek = 1,
    }
    /// <summary>
    /// Retrieves information about a Unicode character. This class cannot be inherited.
    /// </summary>
    public static partial class CharUnicodeInfo
    {
        /// <summary>
        /// Gets the numeric value associated with the specified character.
        /// </summary>
        /// <returns>
        /// The numeric value associated with the specified character.-or- -1, if the specified character
        /// is not a numeric character.
        /// </returns>
        /// <param name="ch">The Unicode character for which to get the numeric value.</param>
        public static double GetNumericValue(char ch) { return default(double); }
        /// <summary>
        /// Gets the numeric value associated with the character at the specified index of the specified
        /// string.
        /// </summary>
        /// <returns>
        /// The numeric value associated with the character at the specified index of the specified string.-or-
        /// -1, if the character at the specified index of the specified string is not a numeric character.
        /// </returns>
        /// <param name="s">
        /// The <see cref="T:System.String" /> containing the Unicode character for which to get the numeric
        /// value.
        /// </param>
        /// <param name="index">The index of the Unicode character for which to get the numeric value.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="s" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index" /> is outside the range of valid indexes in <paramref name="s" />.
        /// </exception>
        public static double GetNumericValue(string s, int index) { return default(double); }
        /// <summary>
        /// Gets the Unicode category of the specified character.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Globalization.UnicodeCategory" /> value indicating the category of the
        /// specified character.
        /// </returns>
        /// <param name="ch">The Unicode character for which to get the Unicode category.</param>
        public static System.Globalization.UnicodeCategory GetUnicodeCategory(char ch) { return default(System.Globalization.UnicodeCategory); }
        /// <summary>
        /// Gets the Unicode category of the character at the specified index of the specified string.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Globalization.UnicodeCategory" /> value indicating the category of the
        /// character at the specified index of the specified string.
        /// </returns>
        /// <param name="s">
        /// The <see cref="T:System.String" /> containing the Unicode character for which to get the Unicode
        /// category.
        /// </param>
        /// <param name="index">The index of the Unicode character for which to get the Unicode category.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="s" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index" /> is outside the range of valid indexes in <paramref name="s" />.
        /// </exception>
        public static System.Globalization.UnicodeCategory GetUnicodeCategory(string s, int index) { return default(System.Globalization.UnicodeCategory); }
    }
    /// <summary>
    /// Implements a set of methods for culture-sensitive string comparisons.
    /// </summary>
    public partial class CompareInfo
    {
        internal CompareInfo() { }
        /// <summary>
        /// Gets the name of the culture used for sorting operations by this
        /// <see cref="T:System.Globalization.CompareInfo" /> object.
        /// </summary>
        /// <returns>
        /// The name of a culture.
        /// </returns>
        public virtual string Name { get { return default(string); } }
        /// <summary>
        /// Compares a section of one string with a section of another string.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer indicating the lexical relationship between the two comparands.Value
        /// Condition zero The two strings are equal. less than zero The specified section of <paramref name="string1" />
        /// is less than the specified section of <paramref name="string2" />. greater
        /// than zero The specified section of <paramref name="string1" /> is greater than the specified
        /// section of <paramref name="string2" />.
        /// </returns>
        /// <param name="string1">The first string to compare.</param>
        /// <param name="offset1">
        /// The zero-based index of the character in <paramref name="string1" /> at which to start comparing.
        /// </param>
        /// <param name="length1">The number of consecutive characters in <paramref name="string1" /> to compare.</param>
        /// <param name="string2">The second string to compare.</param>
        /// <param name="offset2">
        /// The zero-based index of the character in <paramref name="string2" /> at which to start comparing.
        /// </param>
        /// <param name="length2">The number of consecutive characters in <paramref name="string2" /> to compare.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="offset1" /> or <paramref name="length1" /> or <paramref name="offset2" />
        /// or <paramref name="length2" /> is less than zero.-or- <paramref name="offset1" /> is greater
        /// than or equal to the number of characters in <paramref name="string1" />.-or- <paramref name="offset2" />
        /// is greater than or equal to the number of characters in <paramref name="string2" />.-or-
        /// <paramref name="length1" /> is greater than the number of characters from <paramref name="offset1" />
        /// to the end of <paramref name="string1" />.-or- <paramref name="length2" /> is greater than
        /// the number of characters from <paramref name="offset2" /> to the end of <paramref name="string2" />.
        /// </exception>
        public virtual int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2) { return default(int); }
        /// <summary>
        /// Compares a section of one string with a section of another string using the specified
        /// <see cref="T:System.Globalization.CompareOptions" /> value.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer indicating the lexical relationship between the two comparands.Value
        /// Condition zero The two strings are equal. less than zero The specified section of <paramref name="string1" />
        /// is less than the specified section of <paramref name="string2" />. greater
        /// than zero The specified section of <paramref name="string1" /> is greater than the specified
        /// section of <paramref name="string2" />.
        /// </returns>
        /// <param name="string1">The first string to compare.</param>
        /// <param name="offset1">
        /// The zero-based index of the character in <paramref name="string1" /> at which to start comparing.
        /// </param>
        /// <param name="length1">The number of consecutive characters in <paramref name="string1" /> to compare.</param>
        /// <param name="string2">The second string to compare.</param>
        /// <param name="offset2">
        /// The zero-based index of the character in <paramref name="string2" /> at which to start comparing.
        /// </param>
        /// <param name="length2">The number of consecutive characters in <paramref name="string2" /> to compare.</param>
        /// <param name="options">
        /// A value that defines how <paramref name="string1" /> and <paramref name="string2" /> should
        /// be compared. <paramref name="options" /> is either the enumeration value
        /// <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values:
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.
        /// </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="offset1" /> or <paramref name="length1" /> or <paramref name="offset2" />
        /// or <paramref name="length2" /> is less than zero.-or- <paramref name="offset1" /> is greater
        /// than or equal to the number of characters in <paramref name="string1" />.-or- <paramref name="offset2" />
        /// is greater than or equal to the number of characters in <paramref name="string2" />.-or-
        /// <paramref name="length1" /> is greater than the number of characters from <paramref name="offset1" />
        /// to the end of <paramref name="string1" />.-or- <paramref name="length2" /> is greater than
        /// the number of characters from <paramref name="offset2" /> to the end of <paramref name="string2" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </exception>
        public virtual int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2, System.Globalization.CompareOptions options) { return default(int); }
        /// <summary>
        /// Compares the end section of a string with the end section of another string.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer indicating the lexical relationship between the two comparands.Value
        /// Condition zero The two strings are equal. less than zero The specified section of <paramref name="string1" />
        /// is less than the specified section of <paramref name="string2" />. greater
        /// than zero The specified section of <paramref name="string1" /> is greater than the specified
        /// section of <paramref name="string2" />.
        /// </returns>
        /// <param name="string1">The first string to compare.</param>
        /// <param name="offset1">
        /// The zero-based index of the character in <paramref name="string1" /> at which to start comparing.
        /// </param>
        /// <param name="string2">The second string to compare.</param>
        /// <param name="offset2">
        /// The zero-based index of the character in <paramref name="string2" /> at which to start comparing.
        /// </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="offset1" /> or <paramref name="offset2" /> is less than zero.-or- <paramref name="offset1" />
        /// is greater than or equal to the number of characters in <paramref name="string1" />.
        /// -or- <paramref name="offset2" /> is greater than or equal to the number of characters in
        /// <paramref name="string2" />.
        /// </exception>
        public virtual int Compare(string string1, int offset1, string string2, int offset2) { return default(int); }
        /// <summary>
        /// Compares the end section of a string with the end section of another string using the specified
        /// <see cref="T:System.Globalization.CompareOptions" /> value.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer indicating the lexical relationship between the two comparands.Value
        /// Condition zero The two strings are equal. less than zero The specified section of <paramref name="string1" />
        /// is less than the specified section of <paramref name="string2" />. greater
        /// than zero The specified section of <paramref name="string1" /> is greater than the specified
        /// section of <paramref name="string2" />.
        /// </returns>
        /// <param name="string1">The first string to compare.</param>
        /// <param name="offset1">
        /// The zero-based index of the character in <paramref name="string1" /> at which to start comparing.
        /// </param>
        /// <param name="string2">The second string to compare.</param>
        /// <param name="offset2">
        /// The zero-based index of the character in <paramref name="string2" /> at which to start comparing.
        /// </param>
        /// <param name="options">
        /// A value that defines how <paramref name="string1" /> and <paramref name="string2" /> should
        /// be compared. <paramref name="options" /> is either the enumeration value
        /// <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values:
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.
        /// </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="offset1" /> or <paramref name="offset2" /> is less than zero.-or- <paramref name="offset1" />
        /// is greater than or equal to the number of characters in <paramref name="string1" />.
        /// -or- <paramref name="offset2" /> is greater than or equal to the number of characters in
        /// <paramref name="string2" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </exception>
        public virtual int Compare(string string1, int offset1, string string2, int offset2, System.Globalization.CompareOptions options) { return default(int); }
        /// <summary>
        /// Compares two strings.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer indicating the lexical relationship between the two comparands.Value
        /// Condition zero The two strings are equal. less than zero <paramref name="string1" /> is less
        /// than <paramref name="string2" />. greater than zero <paramref name="string1" /> is greater
        /// than <paramref name="string2" />.
        /// </returns>
        /// <param name="string1">The first string to compare.</param>
        /// <param name="string2">The second string to compare.</param>
        public virtual int Compare(string string1, string string2) { return default(int); }
        /// <summary>
        /// Compares two strings using the specified <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer indicating the lexical relationship between the two comparands.Value
        /// Condition zero The two strings are equal. less than zero <paramref name="string1" /> is less
        /// than <paramref name="string2" />. greater than zero <paramref name="string1" /> is greater
        /// than <paramref name="string2" />.
        /// </returns>
        /// <param name="string1">The first string to compare.</param>
        /// <param name="string2">The second string to compare.</param>
        /// <param name="options">
        /// A value that defines how <paramref name="string1" /> and <paramref name="string2" /> should
        /// be compared. <paramref name="options" /> is either the enumeration value
        /// <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values:
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </exception>
        public virtual int Compare(string string1, string string2, System.Globalization.CompareOptions options) { return default(int); }
        /// <summary>
        /// Determines whether the specified object is equal to the current
        /// <see cref="T:System.Globalization.CompareInfo" /> object.
        /// </summary>
        /// <returns>
        /// true if the specified object is equal to the current <see cref="T:System.Globalization.CompareInfo" />
        /// ; otherwise, false.
        /// </returns>
        /// <param name="value">
        /// The object to compare with the current <see cref="T:System.Globalization.CompareInfo" />.
        /// </param>
        public override bool Equals(object value) { return default(bool); }
        /// <summary>
        /// Initializes a new <see cref="T:System.Globalization.CompareInfo" /> object that is associated
        /// with the culture with the specified name.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Globalization.CompareInfo" /> object associated with the culture
        /// with the specified identifier and using string comparison methods in the current
        /// <see cref="T:System.Reflection.Assembly" />.
        /// </returns>
        /// <param name="name">A string representing the culture name.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="name" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="name" /> is an invalid culture name.
        /// </exception>
        public static System.Globalization.CompareInfo GetCompareInfo(string name) { return default(System.Globalization.CompareInfo); }
        /// <summary>
        /// Serves as a hash function for the current <see cref="T:System.Globalization.CompareInfo" />
        /// for hashing algorithms and data structures, such as a hash table.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Globalization.CompareInfo" />.
        /// </returns>
        public override int GetHashCode() { return default(int); }
        /// <summary>
        /// Gets the hash code for a string based on specified comparison options.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer hash code.
        /// </returns>
        /// <param name="source">The string whose hash code is to be returned.</param>
        /// <param name="options">A value that determines how strings are compared.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public virtual int GetHashCode(string source, System.Globalization.CompareOptions options) { return default(int); }
        /// <summary>
        /// Searches for the specified character and returns the zero-based index of the first occurrence
        /// within the entire source string.
        /// </summary>
        /// <returns>
        /// The zero-based index of the first occurrence of <paramref name="value" />, if found, within
        /// <paramref name="source" />; otherwise, -1. Returns 0 (zero) if <paramref name="value" /> is
        /// an ignorable character.
        /// </returns>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The character to locate within <paramref name="source" />.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public virtual int IndexOf(string source, char value) { return default(int); }
        /// <summary>
        /// Searches for the specified character and returns the zero-based index of the first occurrence
        /// within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </summary>
        /// <returns>
        /// The zero-based index of the first occurrence of <paramref name="value" />, if found, within
        /// <paramref name="source" />, using the specified comparison options; otherwise, -1. Returns
        /// 0 (zero) if <paramref name="value" /> is an ignorable character.
        /// </returns>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The character to locate within <paramref name="source" />.</param>
        /// <param name="options">
        /// A value that defines how the strings should be compared. <paramref name="options" /> is either
        /// the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise
        /// combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </exception>
        public virtual int IndexOf(string source, char value, System.Globalization.CompareOptions options) { return default(int); }
        /// <summary>
        /// Searches for the specified character and returns the zero-based index of the first occurrence
        /// within the section of the source string that extends from the specified index to the end of
        /// the string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.
        /// </summary>
        /// <returns>
        /// The zero-based index of the first occurrence of <paramref name="value" />, if found, within
        /// the section of <paramref name="source" /> that extends from <paramref name="startIndex" />
        /// to the end of <paramref name="source" />, using the specified comparison options; otherwise,
        /// -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.
        /// </returns>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The character to locate within <paramref name="source" />.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="options">
        /// A value that defines how <paramref name="source" /> and <paramref name="value" /> should be
        /// compared. <paramref name="options" /> is either the enumeration value
        /// <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values:
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </exception>
        public virtual int IndexOf(string source, char value, int startIndex, System.Globalization.CompareOptions options) { return default(int); }
        /// <summary>
        /// Searches for the specified character and returns the zero-based index of the first occurrence
        /// within the section of the source string that starts at the specified index and contains the specified
        /// number of elements.
        /// </summary>
        /// <returns>
        /// The zero-based index of the first occurrence of <paramref name="value" />, if found, within
        /// the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and
        /// contains the number of elements specified by <paramref name="count" />; otherwise, -1. Returns
        /// <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.
        /// </returns>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The character to locate within <paramref name="source" />.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.
        /// -or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and
        /// <paramref name="count" /> do not specify a valid section in <paramref name="source" />.
        /// </exception>
        public virtual int IndexOf(string source, char value, int startIndex, int count) { return default(int); }
        /// <summary>
        /// Searches for the specified character and returns the zero-based index of the first occurrence
        /// within the section of the source string that starts at the specified index and contains the
        /// specified number of elements using the specified <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </summary>
        /// <returns>
        /// The zero-based index of the first occurrence of <paramref name="value" />, if found, within
        /// the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and
        /// contains the number of elements specified by <paramref name="count" />, using the specified
        /// comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" />
        /// is an ignorable character.
        /// </returns>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The character to locate within <paramref name="source" />.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <param name="options">
        /// A value that defines how <paramref name="source" /> and <paramref name="value" /> should be
        /// compared. <paramref name="options" /> is either the enumeration value
        /// <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values:
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.
        /// -or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and
        /// <paramref name="count" /> do not specify a valid section in <paramref name="source" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </exception>
        public virtual int IndexOf(string source, char value, int startIndex, int count, System.Globalization.CompareOptions options) { return default(int); }
        /// <summary>
        /// Searches for the specified substring and returns the zero-based index of the first occurrence
        /// within the entire source string.
        /// </summary>
        /// <returns>
        /// The zero-based index of the first occurrence of <paramref name="value" />, if found, within
        /// <paramref name="source" />; otherwise, -1. Returns 0 (zero) if <paramref name="value" /> is
        /// an ignorable character.
        /// </returns>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The string to locate within <paramref name="source" />.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.-or- <paramref name="value" /> is null.
        /// </exception>
        public virtual int IndexOf(string source, string value) { return default(int); }
        /// <summary>
        /// Searches for the specified substring and returns the zero-based index of the first occurrence
        /// within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </summary>
        /// <returns>
        /// The zero-based index of the first occurrence of <paramref name="value" />, if found, within
        /// <paramref name="source" />, using the specified comparison options; otherwise, -1. Returns
        /// 0 (zero) if <paramref name="value" /> is an ignorable character.
        /// </returns>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The string to locate within <paramref name="source" />.</param>
        /// <param name="options">
        /// A value that defines how <paramref name="source" /> and <paramref name="value" /> should be
        /// compared. <paramref name="options" /> is either the enumeration value
        /// <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values:
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.-or- <paramref name="value" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </exception>
        public virtual int IndexOf(string source, string value, System.Globalization.CompareOptions options) { return default(int); }
        /// <summary>
        /// Searches for the specified substring and returns the zero-based index of the first occurrence
        /// within the section of the source string that extends from the specified index to the end of
        /// the string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.
        /// </summary>
        /// <returns>
        /// The zero-based index of the first occurrence of <paramref name="value" />, if found, within
        /// the section of <paramref name="source" /> that extends from <paramref name="startIndex" />
        /// to the end of <paramref name="source" />, using the specified comparison options; otherwise,
        /// -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.
        /// </returns>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The string to locate within <paramref name="source" />.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="options">
        /// A value that defines how <paramref name="source" /> and <paramref name="value" /> should be
        /// compared. <paramref name="options" /> is either the enumeration value
        /// <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values:
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.-or- <paramref name="value" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </exception>
        public virtual int IndexOf(string source, string value, int startIndex, System.Globalization.CompareOptions options) { return default(int); }
        /// <summary>
        /// Searches for the specified substring and returns the zero-based index of the first occurrence
        /// within the section of the source string that starts at the specified index and contains the specified
        /// number of elements.
        /// </summary>
        /// <returns>
        /// The zero-based index of the first occurrence of <paramref name="value" />, if found, within
        /// the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and
        /// contains the number of elements specified by <paramref name="count" />; otherwise, -1. Returns
        /// <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.
        /// </returns>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The string to locate within <paramref name="source" />.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.-or- <paramref name="value" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.
        /// -or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and
        /// <paramref name="count" /> do not specify a valid section in <paramref name="source" />.
        /// </exception>
        public virtual int IndexOf(string source, string value, int startIndex, int count) { return default(int); }
        /// <summary>
        /// Searches for the specified substring and returns the zero-based index of the first occurrence
        /// within the section of the source string that starts at the specified index and contains the
        /// specified number of elements using the specified <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </summary>
        /// <returns>
        /// The zero-based index of the first occurrence of <paramref name="value" />, if found, within
        /// the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and
        /// contains the number of elements specified by <paramref name="count" />, using the specified
        /// comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" />
        /// is an ignorable character.
        /// </returns>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The string to locate within <paramref name="source" />.</param>
        /// <param name="startIndex">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <param name="options">
        /// A value that defines how <paramref name="source" /> and <paramref name="value" /> should be
        /// compared. <paramref name="options" /> is either the enumeration value
        /// <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values:
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.-or- <paramref name="value" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.
        /// -or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and
        /// <paramref name="count" /> do not specify a valid section in <paramref name="source" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </exception>
        public virtual int IndexOf(string source, string value, int startIndex, int count, System.Globalization.CompareOptions options) { return default(int); }
        /// <summary>
        /// Determines whether the specified source string starts with the specified prefix.
        /// </summary>
        /// <returns>
        /// true if the length of <paramref name="prefix" /> is less than or equal to the length of
        /// <paramref name="source" /> and <paramref name="source" /> starts with <paramref name="prefix" />; otherwise,
        /// false.
        /// </returns>
        /// <param name="source">The string to search in.</param>
        /// <param name="prefix">The string to compare with the beginning of <paramref name="source" />.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.-or- <paramref name="prefix" /> is null.
        /// </exception>
        public virtual bool IsPrefix(string source, string prefix) { return default(bool); }
        /// <summary>
        /// Determines whether the specified source string starts with the specified prefix using the
        /// specified <see cref="T:System.Globalization.CompareOptions" /> value.
        /// </summary>
        /// <returns>
        /// true if the length of <paramref name="prefix" /> is less than or equal to the length of
        /// <paramref name="source" /> and <paramref name="source" /> starts with <paramref name="prefix" />; otherwise,
        /// false.
        /// </returns>
        /// <param name="source">The string to search in.</param>
        /// <param name="prefix">The string to compare with the beginning of <paramref name="source" />.</param>
        /// <param name="options">
        /// A value that defines how <paramref name="source" /> and <paramref name="prefix" /> should
        /// be compared. <paramref name="options" /> is either the enumeration value
        /// <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values:
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.-or- <paramref name="prefix" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </exception>
        public virtual bool IsPrefix(string source, string prefix, System.Globalization.CompareOptions options) { return default(bool); }
        /// <summary>
        /// Determines whether the specified source string ends with the specified suffix.
        /// </summary>
        /// <returns>
        /// true if the length of <paramref name="suffix" /> is less than or equal to the length of
        /// <paramref name="source" /> and <paramref name="source" /> ends with <paramref name="suffix" />; otherwise,
        /// false.
        /// </returns>
        /// <param name="source">The string to search in.</param>
        /// <param name="suffix">The string to compare with the end of <paramref name="source" />.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.-or- <paramref name="suffix" /> is null.
        /// </exception>
        public virtual bool IsSuffix(string source, string suffix) { return default(bool); }
        /// <summary>
        /// Determines whether the specified source string ends with the specified suffix using the specified
        /// <see cref="T:System.Globalization.CompareOptions" /> value.
        /// </summary>
        /// <returns>
        /// true if the length of <paramref name="suffix" /> is less than or equal to the length of
        /// <paramref name="source" /> and <paramref name="source" /> ends with <paramref name="suffix" />; otherwise,
        /// false.
        /// </returns>
        /// <param name="source">The string to search in.</param>
        /// <param name="suffix">The string to compare with the end of <paramref name="source" />.</param>
        /// <param name="options">
        /// A value that defines how <paramref name="source" /> and <paramref name="suffix" /> should
        /// be compared. <paramref name="options" /> is either the enumeration value
        /// <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values:
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.-or- <paramref name="suffix" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </exception>
        public virtual bool IsSuffix(string source, string suffix, System.Globalization.CompareOptions options) { return default(bool); }
        /// <summary>
        /// Searches for the specified character and returns the zero-based index of the last occurrence
        /// within the entire source string.
        /// </summary>
        /// <returns>
        /// The zero-based index of the last occurrence of <paramref name="value" />, if found, within
        /// <paramref name="source" />; otherwise, -1.
        /// </returns>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The character to locate within <paramref name="source" />.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public virtual int LastIndexOf(string source, char value) { return default(int); }
        /// <summary>
        /// Searches for the specified character and returns the zero-based index of the last occurrence
        /// within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </summary>
        /// <returns>
        /// The zero-based index of the last occurrence of <paramref name="value" />, if found, within
        /// <paramref name="source" />, using the specified comparison options; otherwise, -1.
        /// </returns>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The character to locate within <paramref name="source" />.</param>
        /// <param name="options">
        /// A value that defines how <paramref name="source" /> and <paramref name="value" /> should be
        /// compared. <paramref name="options" /> is either the enumeration value
        /// <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values:
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </exception>
        public virtual int LastIndexOf(string source, char value, System.Globalization.CompareOptions options) { return default(int); }
        /// <summary>
        /// Searches for the specified character and returns the zero-based index of the last occurrence
        /// within the section of the source string that extends from the beginning of the string to the
        /// specified index using the specified <see cref="T:System.Globalization.CompareOptions" /> value.
        /// </summary>
        /// <returns>
        /// The zero-based index of the last occurrence of <paramref name="value" />, if found, within
        /// the section of <paramref name="source" /> that extends from the beginning of <paramref name="source" />
        /// to <paramref name="startIndex" />, using the specified comparison options; otherwise, -1.
        /// Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.
        /// </returns>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The character to locate within <paramref name="source" />.</param>
        /// <param name="startIndex">The zero-based starting index of the backward search.</param>
        /// <param name="options">
        /// A value that defines how <paramref name="source" /> and <paramref name="value" /> should be
        /// compared. <paramref name="options" /> is either the enumeration value
        /// <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values:
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </exception>
        public virtual int LastIndexOf(string source, char value, int startIndex, System.Globalization.CompareOptions options) { return default(int); }
        /// <summary>
        /// Searches for the specified character and returns the zero-based index of the last occurrence
        /// within the section of the source string that contains the specified number of elements and ends at
        /// the specified index.
        /// </summary>
        /// <returns>
        /// The zero-based index of the last occurrence of <paramref name="value" />, if found, within
        /// the section of <paramref name="source" /> that contains the number of elements specified by
        /// <paramref name="count" /> and that ends at <paramref name="startIndex" />; otherwise, -1.
        /// Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.
        /// </returns>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The character to locate within <paramref name="source" />.</param>
        /// <param name="startIndex">The zero-based starting index of the backward search.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.
        /// -or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and
        /// <paramref name="count" /> do not specify a valid section in <paramref name="source" />.
        /// </exception>
        public virtual int LastIndexOf(string source, char value, int startIndex, int count) { return default(int); }
        /// <summary>
        /// Searches for the specified character and returns the zero-based index of the last occurrence
        /// within the section of the source string that contains the specified number of elements and
        /// ends at the specified index using the specified <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </summary>
        /// <returns>
        /// The zero-based index of the last occurrence of <paramref name="value" />, if found, within
        /// the section of <paramref name="source" /> that contains the number of elements specified by
        /// <paramref name="count" /> and that ends at <paramref name="startIndex" />, using the specified
        /// comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" />
        /// is an ignorable character.
        /// </returns>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The character to locate within <paramref name="source" />.</param>
        /// <param name="startIndex">The zero-based starting index of the backward search.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <param name="options">
        /// A value that defines how <paramref name="source" /> and <paramref name="value" /> should be
        /// compared. <paramref name="options" /> is either the enumeration value
        /// <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values:
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.
        /// -or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and
        /// <paramref name="count" /> do not specify a valid section in <paramref name="source" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </exception>
        public virtual int LastIndexOf(string source, char value, int startIndex, int count, System.Globalization.CompareOptions options) { return default(int); }
        /// <summary>
        /// Searches for the specified substring and returns the zero-based index of the last occurrence
        /// within the entire source string.
        /// </summary>
        /// <returns>
        /// The zero-based index of the last occurrence of <paramref name="value" />, if found, within
        /// <paramref name="source" />; otherwise, -1.
        /// </returns>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The string to locate within <paramref name="source" />.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.-or- <paramref name="value" /> is null.
        /// </exception>
        public virtual int LastIndexOf(string source, string value) { return default(int); }
        /// <summary>
        /// Searches for the specified substring and returns the zero-based index of the last occurrence
        /// within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </summary>
        /// <returns>
        /// The zero-based index of the last occurrence of <paramref name="value" />, if found, within
        /// <paramref name="source" />, using the specified comparison options; otherwise, -1.
        /// </returns>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The string to locate within <paramref name="source" />.</param>
        /// <param name="options">
        /// A value that defines how <paramref name="source" /> and <paramref name="value" /> should be
        /// compared. <paramref name="options" /> is either the enumeration value
        /// <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values:
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.-or- <paramref name="value" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </exception>
        public virtual int LastIndexOf(string source, string value, System.Globalization.CompareOptions options) { return default(int); }
        /// <summary>
        /// Searches for the specified substring and returns the zero-based index of the last occurrence
        /// within the section of the source string that extends from the beginning of the string to the
        /// specified index using the specified <see cref="T:System.Globalization.CompareOptions" /> value.
        /// </summary>
        /// <returns>
        /// The zero-based index of the last occurrence of <paramref name="value" />, if found, within
        /// the section of <paramref name="source" /> that extends from the beginning of <paramref name="source" />
        /// to <paramref name="startIndex" />, using the specified comparison options; otherwise, -1.
        /// Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.
        /// </returns>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The string to locate within <paramref name="source" />.</param>
        /// <param name="startIndex">The zero-based starting index of the backward search.</param>
        /// <param name="options">
        /// A value that defines how <paramref name="source" /> and <paramref name="value" /> should be
        /// compared. <paramref name="options" /> is either the enumeration value
        /// <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values:
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.-or- <paramref name="value" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </exception>
        public virtual int LastIndexOf(string source, string value, int startIndex, System.Globalization.CompareOptions options) { return default(int); }
        /// <summary>
        /// Searches for the specified substring and returns the zero-based index of the last occurrence
        /// within the section of the source string that contains the specified number of elements and ends at
        /// the specified index.
        /// </summary>
        /// <returns>
        /// The zero-based index of the last occurrence of <paramref name="value" />, if found, within
        /// the section of <paramref name="source" /> that contains the number of elements specified by
        /// <paramref name="count" /> and that ends at <paramref name="startIndex" />; otherwise, -1.
        /// Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.
        /// </returns>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The string to locate within <paramref name="source" />.</param>
        /// <param name="startIndex">The zero-based starting index of the backward search.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.-or- <paramref name="value" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.
        /// -or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and
        /// <paramref name="count" /> do not specify a valid section in <paramref name="source" />.
        /// </exception>
        public virtual int LastIndexOf(string source, string value, int startIndex, int count) { return default(int); }
        /// <summary>
        /// Searches for the specified substring and returns the zero-based index of the last occurrence
        /// within the section of the source string that contains the specified number of elements and
        /// ends at the specified index using the specified <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </summary>
        /// <returns>
        /// The zero-based index of the last occurrence of <paramref name="value" />, if found, within
        /// the section of <paramref name="source" /> that contains the number of elements specified by
        /// <paramref name="count" /> and that ends at <paramref name="startIndex" />, using the specified
        /// comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" />
        /// is an ignorable character.
        /// </returns>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The string to locate within <paramref name="source" />.</param>
        /// <param name="startIndex">The zero-based starting index of the backward search.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <param name="options">
        /// A value that defines how <paramref name="source" /> and <paramref name="value" /> should be
        /// compared. <paramref name="options" /> is either the enumeration value
        /// <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values:
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />,
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and
        /// <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.-or- <paramref name="value" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.
        /// -or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and
        /// <paramref name="count" /> do not specify a valid section in <paramref name="source" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" />
        /// value.
        /// </exception>
        public virtual int LastIndexOf(string source, string value, int startIndex, int count, System.Globalization.CompareOptions options) { return default(int); }
        /// <summary>
        /// Returns a string that represents the current <see cref="T:System.Globalization.CompareInfo" />
        /// object.
        /// </summary>
        /// <returns>
        /// A string that represents the current <see cref="T:System.Globalization.CompareInfo" /> object.
        /// </returns>
        public override string ToString() { return default(string); }
    }
    /// <summary>
    /// Defines the string comparison options to use with <see cref="T:System.Globalization.CompareInfo" />.
    /// </summary>
    [System.FlagsAttribute]
    public enum CompareOptions
    {
        /// <summary>
        /// Indicates that the string comparison must ignore case.
        /// </summary>
        IgnoreCase = 1,
        /// <summary>
        /// Indicates that the string comparison must ignore the Kana type. Kana type refers to Japanese
        /// hiragana and katakana characters, which represent phonetic sounds in the Japanese language. Hiragana
        /// is used for native Japanese expressions and words, while katakana is used for words borrowed from
        /// other languages, such as "computer" or "Internet". A phonetic sound can be expressed in both hiragana
        /// and katakana. If this value is selected, the hiragana character for one sound is considered equal
        /// to the katakana character for the same sound.
        /// </summary>
        IgnoreKanaType = 8,
        /// <summary>
        /// Indicates that the string comparison must ignore nonspacing combining characters, such as diacritics.
        /// The Unicode Standard defines combining characters as characters that are combined with base
        /// characters to produce a new character. Nonspacing combining characters do not occupy a spacing position
        /// by themselves when rendered.
        /// </summary>
        IgnoreNonSpace = 2,
        /// <summary>
        /// Indicates that the string comparison must ignore symbols, such as white-space characters, punctuation,
        /// currency symbols, the percent sign, mathematical symbols, the ampersand, and so on.
        /// </summary>
        IgnoreSymbols = 4,
        /// <summary>
        /// Indicates that the string comparison must ignore the character width. For example, Japanese
        /// katakana characters can be written as full-width or half-width. If this value is selected, the katakana
        /// characters written as full-width are considered equal to the same characters written as half-width.
        /// </summary>
        IgnoreWidth = 16,
        /// <summary>
        /// Indicates the default option settings for string comparisons.
        /// </summary>
        None = 0,
        /// <summary>
        /// Indicates that the string comparison must use successive Unicode UTF-16 encoded values of
        /// the string (code unit by code unit comparison), leading to a fast comparison but one that
        /// is culture-insensitive. A string starting with a code unit XXXX16 comes before a string starting
        /// with YYYY16, if XXXX16 is less than YYYY16. This value cannot be combined with other
        /// <see cref="T:System.Globalization.CompareOptions" /> values and must be used alone.
        /// </summary>
        Ordinal = 1073741824,
        /// <summary>
        /// String comparison must ignore case, then perform an ordinal comparison. This technique is equivalent
        /// to converting the string to uppercase using the invariant culture and then performing an ordinal
        /// comparison on the result.
        /// </summary>
        OrdinalIgnoreCase = 268435456,
        /// <summary>
        /// Indicates that the string comparison must use the string sort algorithm. In a string sort,
        /// the hyphen and the apostrophe, as well as other nonalphanumeric symbols, come before alphanumeric characters.
        /// </summary>
        StringSort = 536870912,
    }
    /// <summary>
    /// Provides information about a specific culture (called a locale for unmanaged code development).
    /// The information includes the names for the culture, the writing system, the calendar used, the
    /// sort order of strings, and formatting for dates and numbers.
    /// </summary>
    public partial class CultureInfo : System.IFormatProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class
        /// based on the culture specified by name.
        /// </summary>
        /// <param name="name">
        /// A predefined <see cref="T:System.Globalization.CultureInfo" /> name,
        /// <see cref="P:System.Globalization.CultureInfo.Name" /> of an existing <see cref="T:System.Globalization.CultureInfo" />, or Windows-only culture
        /// name. <paramref name="name" /> is not case-sensitive.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="name" /> is null.</exception>
        /// <exception cref="T:System.Globalization.CultureNotFoundException">
        /// <paramref name="name" /> is not a valid culture name. For more information, see the Notes
        /// to Callers section.
        /// </exception>
        public CultureInfo(string name) { }
        /// <summary>
        /// Gets the default calendar used by the culture.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Globalization.Calendar" /> that represents the default calendar used
        /// by the culture.
        /// </returns>
        public virtual System.Globalization.Calendar Calendar { get { return default(System.Globalization.Calendar); } }
        /// <summary>
        /// Gets the <see cref="T:System.Globalization.CompareInfo" /> that defines how to compare strings
        /// for the culture.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Globalization.CompareInfo" /> that defines how to compare strings
        /// for the culture.
        /// </returns>
        public virtual System.Globalization.CompareInfo CompareInfo { get { return default(System.Globalization.CompareInfo); } }
        /// <summary>
        /// Gets or sets the <see cref="T:System.Globalization.CultureInfo" /> object that represents
        /// the culture used by the current thread.
        /// </summary>
        /// <returns>
        /// An object that represents the culture used by the current thread.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is set to null.</exception>
        public static System.Globalization.CultureInfo CurrentCulture { get { return default(System.Globalization.CultureInfo); } set { } }
        /// <summary>
        /// Gets or sets the <see cref="T:System.Globalization.CultureInfo" /> object that represents
        /// the current user interface culture used by the Resource Manager to look up culture-specific
        /// resources at run time.
        /// </summary>
        /// <returns>
        /// The culture used by the Resource Manager to look up culture-specific resources at run time.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is set to null.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// The property is set to a culture name that cannot be used to locate a resource file. Resource
        /// filenames can include only letters, numbers, hyphens, or underscores.
        /// </exception>
        public static System.Globalization.CultureInfo CurrentUICulture { get { return default(System.Globalization.CultureInfo); } set { } }
        /// <summary>
        /// Gets or sets a <see cref="T:System.Globalization.DateTimeFormatInfo" /> that defines the culturally
        /// appropriate format of displaying dates and times.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Globalization.DateTimeFormatInfo" /> that defines the culturally appropriate
        /// format of displaying dates and times.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The <see cref="P:System.Globalization.CultureInfo.DateTimeFormat" /> property or any of the
        /// <see cref="T:System.Globalization.DateTimeFormatInfo" /> properties is set, and the
        /// <see cref="T:System.Globalization.CultureInfo" /> is read-only.
        /// </exception>
        public virtual System.Globalization.DateTimeFormatInfo DateTimeFormat { get { return default(System.Globalization.DateTimeFormatInfo); } set { } }
        /// <summary>
        /// Gets or sets the default culture for threads in the current application domain.
        /// </summary>
        /// <returns>
        /// The default culture for threads in the current application domain, or null if the current system
        /// culture is the default thread culture in the application domain.
        /// </returns>
        public static System.Globalization.CultureInfo DefaultThreadCurrentCulture { get { return default(System.Globalization.CultureInfo); } set { } }
        /// <summary>
        /// Gets or sets the default UI culture for threads in the current application domain.
        /// </summary>
        /// <returns>
        /// The default UI culture for threads in the current application domain, or null if the current
        /// system UI culture is the default thread UI culture in the application domain.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        /// In a set operation, the <see cref="P:System.Globalization.CultureInfo.Name" /> property value
        /// is invalid.
        /// </exception>
        public static System.Globalization.CultureInfo DefaultThreadCurrentUICulture { get { return default(System.Globalization.CultureInfo); } set { } }
        /// <summary>
        /// Gets the full localized culture name.
        /// </summary>
        /// <returns>
        /// The full localized culture name in the format languagefull [country/regionfull], where languagefull
        /// is the full name of the language and country/regionfull is the full name of the country/region.
        /// </returns>
        public virtual string DisplayName { get { return default(string); } }
        /// <summary>
        /// Gets the culture name in the format languagefull [country/regionfull] in English.
        /// </summary>
        /// <returns>
        /// The culture name in the format languagefull [country/regionfull] in English, where languagefull
        /// is the full name of the language and country/regionfull is the full name of the country/region.
        /// </returns>
        public virtual string EnglishName { get { return default(string); } }
        /// <summary>
        /// Gets the <see cref="T:System.Globalization.CultureInfo" /> object that is culture-independent
        /// (invariant).
        /// </summary>
        /// <returns>
        /// The object that is culture-independent (invariant).
        /// </returns>
        public static System.Globalization.CultureInfo InvariantCulture { get { return default(System.Globalization.CultureInfo); } }
        /// <summary>
        /// Gets a value indicating whether the current <see cref="T:System.Globalization.CultureInfo" />
        /// represents a neutral culture.
        /// </summary>
        /// <returns>
        /// true if the current <see cref="T:System.Globalization.CultureInfo" /> represents a neutral
        /// culture; otherwise, false.
        /// </returns>
        public virtual bool IsNeutralCulture { get { return default(bool); } }
        /// <summary>
        /// Gets a value indicating whether the current <see cref="T:System.Globalization.CultureInfo" />
        /// is read-only.
        /// </summary>
        /// <returns>
        /// true if the current <see cref="T:System.Globalization.CultureInfo" /> is read-only; otherwise,
        /// false. The default is false.
        /// </returns>
        public bool IsReadOnly { get { return default(bool); } }
        /// <summary>
        /// Gets the culture name in the format languagecode2-country/regioncode2.
        /// </summary>
        /// <returns>
        /// The culture name in the format languagecode2-country/regioncode2. languagecode2 is a lowercase
        /// two-letter code derived from ISO 639-1. country/regioncode2 is derived from ISO 3166 and usually
        /// consists of two uppercase letters, or a BCP-47 language tag.
        /// </returns>
        public virtual string Name { get { return default(string); } }
        /// <summary>
        /// Gets the culture name, consisting of the language, the country/region, and the optional script,
        /// that the culture is set to display.
        /// </summary>
        /// <returns>
        /// The culture name. consisting of the full name of the language, the full name of the country/region,
        /// and the optional script. The format is discussed in the description of the
        /// <see cref="T:System.Globalization.CultureInfo" /> class.
        /// </returns>
        public virtual string NativeName { get { return default(string); } }
        /// <summary>
        /// Gets or sets a <see cref="T:System.Globalization.NumberFormatInfo" /> that defines the culturally
        /// appropriate format of displaying numbers, currency, and percentage.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Globalization.NumberFormatInfo" /> that defines the culturally appropriate
        /// format of displaying numbers, currency, and percentage.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The <see cref="P:System.Globalization.CultureInfo.NumberFormat" /> property or any of the
        /// <see cref="T:System.Globalization.NumberFormatInfo" /> properties is set, and the
        /// <see cref="T:System.Globalization.CultureInfo" /> is read-only.
        /// </exception>
        public virtual System.Globalization.NumberFormatInfo NumberFormat { get { return default(System.Globalization.NumberFormatInfo); } set { } }
        /// <summary>
        /// Gets the list of calendars that can be used by the culture.
        /// </summary>
        /// <returns>
        /// An array of type <see cref="T:System.Globalization.Calendar" /> that represents the calendars
        /// that can be used by the culture represented by the current <see cref="T:System.Globalization.CultureInfo" />.
        /// </returns>
        public virtual System.Globalization.Calendar[] OptionalCalendars { get { return default(System.Globalization.Calendar[]); } }
        /// <summary>
        /// Gets the <see cref="T:System.Globalization.CultureInfo" /> that represents the parent culture
        /// of the current <see cref="T:System.Globalization.CultureInfo" />.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Globalization.CultureInfo" /> that represents the parent culture of
        /// the current <see cref="T:System.Globalization.CultureInfo" />.
        /// </returns>
        public virtual System.Globalization.CultureInfo Parent { get { return default(System.Globalization.CultureInfo); } }
        /// <summary>
        /// Gets the <see cref="T:System.Globalization.TextInfo" /> that defines the writing system associated
        /// with the culture.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Globalization.TextInfo" /> that defines the writing system associated
        /// with the culture.
        /// </returns>
        public virtual System.Globalization.TextInfo TextInfo { get { return default(System.Globalization.TextInfo); } }
        /// <summary>
        /// Gets the ISO 639-1 two-letter code for the language of the current
        /// <see cref="T:System.Globalization.CultureInfo" />.
        /// </summary>
        /// <returns>
        /// The ISO 639-1 two-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.
        /// </returns>
        public virtual string TwoLetterISOLanguageName { get { return default(string); } }
        /// <summary>
        /// Creates a copy of the current <see cref="T:System.Globalization.CultureInfo" />.
        /// </summary>
        /// <returns>
        /// A copy of the current <see cref="T:System.Globalization.CultureInfo" />.
        /// </returns>
        public virtual object Clone() { return default(object); }
        /// <summary>
        /// Determines whether the specified object is the same culture as the current
        /// <see cref="T:System.Globalization.CultureInfo" />.
        /// </summary>
        /// <returns>
        /// true if <paramref name="value" /> is the same culture as the current
        /// <see cref="T:System.Globalization.CultureInfo" />; otherwise, false.
        /// </returns>
        /// <param name="value">
        /// The object to compare with the current <see cref="T:System.Globalization.CultureInfo" />.
        /// </param>
        public override bool Equals(object value) { return default(bool); }
        /// <summary>
        /// Gets an object that defines how to format the specified type.
        /// </summary>
        /// <returns>
        /// The value of the <see cref="P:System.Globalization.CultureInfo.NumberFormat" /> property,
        /// which is a <see cref="T:System.Globalization.NumberFormatInfo" /> containing the default number
        /// format information for the current <see cref="T:System.Globalization.CultureInfo" />, if <paramref name="formatType" />
        /// is the <see cref="T:System.Type" /> object for the <see cref="T:System.Globalization.NumberFormatInfo" />
        /// class.-or- The value of the <see cref="P:System.Globalization.CultureInfo.DateTimeFormat" />
        /// property, which is a <see cref="T:System.Globalization.DateTimeFormatInfo" /> containing
        /// the default date and time format information for the current <see cref="T:System.Globalization.CultureInfo" />,
        /// if <paramref name="formatType" /> is the <see cref="T:System.Type" /> object for the
        /// <see cref="T:System.Globalization.DateTimeFormatInfo" /> class.-or- null, if <paramref name="formatType" />
        /// is any other object.
        /// </returns>
        /// <param name="formatType">
        /// The <see cref="T:System.Type" /> for which to get a formatting object. This method only supports
        /// the <see cref="T:System.Globalization.NumberFormatInfo" /> and
        /// <see cref="T:System.Globalization.DateTimeFormatInfo" /> types.
        /// </param>
        public virtual object GetFormat(System.Type formatType) { return default(object); }
        /// <summary>
        /// Serves as a hash function for the current <see cref="T:System.Globalization.CultureInfo" />,
        /// suitable for hashing algorithms and data structures, such as a hash table.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Globalization.CultureInfo" />.
        /// </returns>
        public override int GetHashCode() { return default(int); }
        /// <summary>
        /// Returns a read-only wrapper around the specified <see cref="T:System.Globalization.CultureInfo" />
        /// object.
        /// </summary>
        /// <returns>
        /// A read-only <see cref="T:System.Globalization.CultureInfo" /> wrapper around <paramref name="ci" />.
        /// </returns>
        /// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object to wrap.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="ci" /> is null.</exception>
        public static System.Globalization.CultureInfo ReadOnly(System.Globalization.CultureInfo ci) { return default(System.Globalization.CultureInfo); }
        /// <summary>
        /// Returns a string containing the name of the current <see cref="T:System.Globalization.CultureInfo" />
        /// in the format languagecode2-country/regioncode2.
        /// </summary>
        /// <returns>
        /// A string containing the name of the current <see cref="T:System.Globalization.CultureInfo" />.
        /// </returns>
        public override string ToString() { return default(string); }
    }
    /// <summary>
    /// The exception that is thrown when a method is invoked which attempts to construct a culture
    /// that is not available on the machine.
    /// </summary>
    public partial class CultureNotFoundException : System.ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" />
        /// class with its message string set to a system-supplied message.
        /// </summary>
        public CultureNotFoundException() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" />
        /// class with the specified error message.
        /// </summary>
        /// <param name="message">The error message to display with this exception.</param>
        public CultureNotFoundException(string message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" />
        /// class with a specified error message and a reference to the inner exception that is the
        /// cause of this exception.
        /// </summary>
        /// <param name="message">The error message to display with this exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception. If the <paramref name="innerException" />
        /// parameter is not a null reference, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        public CultureNotFoundException(string message, System.Exception innerException) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" />
        /// class with a specified error message and the name of the parameter that is the cause this
        /// exception.
        /// </summary>
        /// <param name="paramName">The name of the parameter that is the cause of the current exception.</param>
        /// <param name="message">The error message to display with this exception.</param>
        public CultureNotFoundException(string paramName, string message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" />
        /// class with a specified error message, the invalid Culture Name, and a reference to the
        /// inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message to display with this exception.</param>
        /// <param name="invalidCultureName">The Culture Name that cannot be found.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception. If the <paramref name="innerException" />
        /// parameter is not a null reference, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        public CultureNotFoundException(string message, string invalidCultureName, System.Exception innerException) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" />
        /// class with a specified error message, the invalid Culture Name, and the name of the parameter
        /// that is the cause this exception.
        /// </summary>
        /// <param name="paramName">The name of the parameter that is the cause the current exception.</param>
        /// <param name="invalidCultureName">The Culture Name that cannot be found.</param>
        /// <param name="message">The error message to display with this exception.</param>
        public CultureNotFoundException(string paramName, string invalidCultureName, string message) { }
        /// <summary>
        /// Gets the culture name that cannot be found.
        /// </summary>
        /// <returns>
        /// The invalid culture name.
        /// </returns>
        public virtual string InvalidCultureName { get { return default(string); } }
        /// <summary>
        /// Gets the error message that explains the reason for the exception.
        /// </summary>
        /// <returns>
        /// A text string describing the details of the exception.
        /// </returns>
        public override string Message { get { return default(string); } }
    }
    /// <summary>
    /// Provides culture-specific information about the format of date and time values.
    /// </summary>
    public sealed partial class DateTimeFormatInfo : System.IFormatProvider
    {
        /// <summary>
        /// Initializes a new writable instance of the <see cref="T:System.Globalization.DateTimeFormatInfo" />
        /// class that is culture-independent (invariant).
        /// </summary>
        public DateTimeFormatInfo() { }
        /// <summary>
        /// Gets or sets a one-dimensional array of type <see cref="T:System.String" /> containing the
        /// culture-specific abbreviated names of the days of the week.
        /// </summary>
        /// <returns>
        /// A one-dimensional array of type <see cref="T:System.String" /> containing the culture-specific
        /// abbreviated names of the days of the week. The array for
        /// <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> contains "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", and "Sat".
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// The property is being set to an array that is multidimensional or that has a length that is
        /// not exactly 7.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" />
        /// object is read-only.
        /// </exception>
        public string[] AbbreviatedDayNames { get { return default(string[]); } set { } }
        /// <summary>
        /// Gets or sets a string array of abbreviated month names associated with the current
        /// <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.
        /// </summary>
        /// <returns>
        /// An array of abbreviated month names.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        /// In a set operation, the array is multidimensional or has a length that is not exactly 13.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// In a set operation, the array or one of the elements of the array is null.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// In a set operation, the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public string[] AbbreviatedMonthGenitiveNames { get { return default(string[]); } set { } }
        /// <summary>
        /// Gets or sets a one-dimensional string array that contains the culture-specific abbreviated
        /// names of the months.
        /// </summary>
        /// <returns>
        /// A one-dimensional string array with 13 elements that contains the culture-specific abbreviated
        /// names of the months. For 12-month calendars, the 13th element of the array is an empty string.
        /// The array for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> contains
        /// "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", and "".
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// The property is being set to an array that is multidimensional or that has a length that is
        /// not exactly 13.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" />
        /// object is read-only.
        /// </exception>
        public string[] AbbreviatedMonthNames { get { return default(string[]); } set { } }
        /// <summary>
        /// Gets or sets the string designator for hours that are "ante meridiem" (before noon).
        /// </summary>
        /// <returns>
        /// The string designator for hours that are ante meridiem. The default for
        /// <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is "AM".
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" />
        /// object is read-only.
        /// </exception>
        public string AMDesignator { get { return default(string); } set { } }
        /// <summary>
        /// Gets or sets the calendar to use for the current culture.
        /// </summary>
        /// <returns>
        /// The calendar to use for the current culture. The default for
        /// <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is a <see cref="T:System.Globalization.GregorianCalendar" /> object.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// The property is being set to a <see cref="T:System.Globalization.Calendar" /> object that
        /// is not valid for the current culture.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" />
        /// object is read-only.
        /// </exception>
        public System.Globalization.Calendar Calendar { get { return default(System.Globalization.Calendar); } set { } }
        /// <summary>
        /// Gets or sets a value that specifies which rule is used to determine the first calendar week
        /// of the year.
        /// </summary>
        /// <returns>
        /// A value that determines the first calendar week of the year. The default for
        /// <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is <see cref="F:System.Globalization.CalendarWeekRule.FirstDay" />.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// The property is being set to a value that is not a valid <see cref="T:System.Globalization.CalendarWeekRule" />
        /// value.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// In a set operation, the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public System.Globalization.CalendarWeekRule CalendarWeekRule { get { return default(System.Globalization.CalendarWeekRule); } set { } }
        /// <summary>
        /// Gets a read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> object that formats
        /// values based on the current culture.
        /// </summary>
        /// <returns>
        /// A read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> object based on the
        /// <see cref="T:System.Globalization.CultureInfo" /> object for the current thread.
        /// </returns>
        public static System.Globalization.DateTimeFormatInfo CurrentInfo { get { return default(System.Globalization.DateTimeFormatInfo); } }
        /// <summary>
        /// Gets or sets a one-dimensional string array that contains the culture-specific full names of
        /// the days of the week.
        /// </summary>
        /// <returns>
        /// A one-dimensional string array that contains the culture-specific full names of the days of
        /// the week. The array for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" />
        /// contains "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", and "Saturday".
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// The property is being set to an array that is multidimensional or that has a length that is
        /// not exactly 7.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" />
        /// object is read-only.
        /// </exception>
        public string[] DayNames { get { return default(string[]); } set { } }
        /// <summary>
        /// Gets or sets the first day of the week.
        /// </summary>
        /// <returns>
        /// An enumeration value that represents the first day of the week. The default for
        /// <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is <see cref="F:System.DayOfWeek.Sunday" />.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// The property is being set to a value that is not a valid <see cref="T:System.DayOfWeek" />
        /// value.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" />
        /// object is read-only.
        /// </exception>
        public System.DayOfWeek FirstDayOfWeek { get { return default(System.DayOfWeek); } set { } }
        /// <summary>
        /// Gets or sets the custom format string for a long date and long time value.
        /// </summary>
        /// <returns>
        /// The custom format string for a long date and long time value.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" />
        /// object is read-only.
        /// </exception>
        public string FullDateTimePattern { get { return default(string); } set { } }
        /// <summary>
        /// Gets the default read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> object
        /// that is culture-independent (invariant).
        /// </summary>
        /// <returns>
        /// A read-only object that is culture-independent (invariant).
        /// </returns>
        public static System.Globalization.DateTimeFormatInfo InvariantInfo { get { return default(System.Globalization.DateTimeFormatInfo); } }
        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Globalization.DateTimeFormatInfo" />
        /// object is read-only.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object is read-only;
        /// otherwise, false.
        /// </returns>
        public bool IsReadOnly { get { return default(bool); } }
        /// <summary>
        /// Gets or sets the custom format string for a long date value.
        /// </summary>
        /// <returns>
        /// The custom format string for a long date value.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" />
        /// object is read-only.
        /// </exception>
        public string LongDatePattern { get { return default(string); } set { } }
        /// <summary>
        /// Gets or sets the custom format string for a long time value.
        /// </summary>
        /// <returns>
        /// The format pattern for a long time value.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" />
        /// object is read-only.
        /// </exception>
        public string LongTimePattern { get { return default(string); } set { } }
        /// <summary>
        /// Gets or sets the custom format string for a month and day value.
        /// </summary>
        /// <returns>
        /// The custom format string for a month and day value.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" />
        /// object is read-only.
        /// </exception>
        public string MonthDayPattern { get { return default(string); } set { } }
        /// <summary>
        /// Gets or sets a string array of month names associated with the current
        /// <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.
        /// </summary>
        /// <returns>
        /// A string array of month names.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        /// In a set operation, the array is multidimensional or has a length that is not exactly 13.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// In a set operation, the array or one of its elements is null.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// In a set operation, the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public string[] MonthGenitiveNames { get { return default(string[]); } set { } }
        /// <summary>
        /// Gets or sets a one-dimensional array of type <see cref="T:System.String" /> containing the
        /// culture-specific full names of the months.
        /// </summary>
        /// <returns>
        /// A one-dimensional array of type <see cref="T:System.String" /> containing the culture-specific
        /// full names of the months. In a 12-month calendar, the 13th element of the array is an empty
        /// string. The array for <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" />
        /// contains "January", "February", "March", "April", "May", "June", "July", "August", "September",
        /// "October", "November", "December", and "".
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// The property is being set to an array that is multidimensional or that has a length that is
        /// not exactly 13.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" />
        /// object is read-only.
        /// </exception>
        public string[] MonthNames { get { return default(string[]); } set { } }
        /// <summary>
        /// Gets or sets the string designator for hours that are "post meridiem" (after noon).
        /// </summary>
        /// <returns>
        /// The string designator for hours that are "post meridiem" (after noon). The default for
        /// <see cref="P:System.Globalization.DateTimeFormatInfo.InvariantInfo" /> is "PM".
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" />
        /// object is read-only.
        /// </exception>
        public string PMDesignator { get { return default(string); } set { } }
        /// <summary>
        /// Gets the custom format string for a time value that is based on the Internet Engineering Task
        /// Force (IETF) Request for Comments (RFC) 1123 specification.
        /// </summary>
        /// <returns>
        /// The custom format string for a time value that is based on the IETF RFC 1123 specification.
        /// </returns>
        public string RFC1123Pattern { get { return default(string); } }
        /// <summary>
        /// Gets or sets the custom format string for a short date value.
        /// </summary>
        /// <returns>
        /// The custom format string for a short date value.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" />
        /// object is read-only.
        /// </exception>
        public string ShortDatePattern { get { return default(string); } set { } }
        /// <summary>
        /// Gets or sets a string array of the shortest unique abbreviated day names associated with the
        /// current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.
        /// </summary>
        /// <returns>
        /// A string array of day names.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        /// In a set operation, the array does not have exactly seven elements.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// In a set operation, the value array or one of the elements of the value array is null.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// In a set operation, the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public string[] ShortestDayNames { get { return default(string[]); } set { } }
        /// <summary>
        /// Gets or sets the custom format string for a short time value.
        /// </summary>
        /// <returns>
        /// The custom format string for a short time value.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" />
        /// object is read-only.
        /// </exception>
        public string ShortTimePattern { get { return default(string); } set { } }
        /// <summary>
        /// Gets the custom format string for a sortable date and time value.
        /// </summary>
        /// <returns>
        /// The custom format string for a sortable date and time value.
        /// </returns>
        public string SortableDateTimePattern { get { return default(string); } }
        /// <summary>
        /// Gets the custom format string for a universal, sortable date and time string.
        /// </summary>
        /// <returns>
        /// The custom format string for a universal, sortable date and time string.
        /// </returns>
        public string UniversalSortableDateTimePattern { get { return default(string); } }
        /// <summary>
        /// Gets or sets the custom format string for a year and month value.
        /// </summary>
        /// <returns>
        /// The custom format string for a year and month value.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.DateTimeFormatInfo" />
        /// object is read-only.
        /// </exception>
        public string YearMonthPattern { get { return default(string); } set { } }
        /// <summary>
        /// Creates a shallow copy of the <see cref="T:System.Globalization.DateTimeFormatInfo" />.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Globalization.DateTimeFormatInfo" /> object copied from the original
        /// <see cref="T:System.Globalization.DateTimeFormatInfo" />.
        /// </returns>
        public object Clone() { return default(object); }
        /// <summary>
        /// Returns the culture-specific abbreviated name of the specified day of the week based on the
        /// culture associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" />
        /// object.
        /// </summary>
        /// <returns>
        /// The culture-specific abbreviated name of the day of the week represented by <paramref name="dayofweek" />.
        /// </returns>
        /// <param name="dayofweek">A <see cref="T:System.DayOfWeek" /> value.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="dayofweek" /> is not a valid <see cref="T:System.DayOfWeek" /> value.
        /// </exception>
        public string GetAbbreviatedDayName(System.DayOfWeek dayofweek) { return default(string); }
        /// <summary>
        /// Returns the string containing the abbreviated name of the specified era, if an abbreviation
        /// exists.
        /// </summary>
        /// <returns>
        /// A string containing the abbreviated name of the specified era, if an abbreviation exists.-or-
        /// A string containing the full name of the era, if an abbreviation does not exist.
        /// </returns>
        /// <param name="era">The integer representing the era.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="era" /> does not represent a valid era in the calendar specified in the
        /// <see cref="P:System.Globalization.DateTimeFormatInfo.Calendar" /> property.
        /// </exception>
        public string GetAbbreviatedEraName(int era) { return default(string); }
        /// <summary>
        /// Returns the culture-specific abbreviated name of the specified month based on the culture
        /// associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.
        /// </summary>
        /// <returns>
        /// The culture-specific abbreviated name of the month represented by <paramref name="month" />.
        /// </returns>
        /// <param name="month">An integer from 1 through 13 representing the name of the month to retrieve.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="month" /> is less than 1 or greater than 13.
        /// </exception>
        public string GetAbbreviatedMonthName(int month) { return default(string); }
        /// <summary>
        /// Returns the culture-specific full name of the specified day of the week based on the culture
        /// associated with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.
        /// </summary>
        /// <returns>
        /// The culture-specific full name of the day of the week represented by <paramref name="dayofweek" />.
        /// </returns>
        /// <param name="dayofweek">A <see cref="T:System.DayOfWeek" /> value.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="dayofweek" /> is not a valid <see cref="T:System.DayOfWeek" /> value.
        /// </exception>
        public string GetDayName(System.DayOfWeek dayofweek) { return default(string); }
        /// <summary>
        /// Returns the integer representing the specified era.
        /// </summary>
        /// <returns>
        /// The integer representing the era, if <paramref name="eraName" /> is valid; otherwise, -1.
        /// </returns>
        /// <param name="eraName">The string containing the name of the era.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="eraName" /> is null.</exception>
        public int GetEra(string eraName) { return default(int); }
        /// <summary>
        /// Returns the string containing the name of the specified era.
        /// </summary>
        /// <returns>
        /// A string containing the name of the era.
        /// </returns>
        /// <param name="era">The integer representing the era.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="era" /> does not represent a valid era in the calendar specified in the
        /// <see cref="P:System.Globalization.DateTimeFormatInfo.Calendar" /> property.
        /// </exception>
        public string GetEraName(int era) { return default(string); }
        /// <summary>
        /// Returns an object of the specified type that provides a date and time  formatting service.
        /// </summary>
        /// <returns>
        /// The current  object, if <paramref name="formatType" /> is the same as the type of the current
        /// <see cref="T:System.Globalization.DateTimeFormatInfo" />; otherwise, null.
        /// </returns>
        /// <param name="formatType">The type of the required formatting service.</param>
        public object GetFormat(System.Type formatType) { return default(object); }
        /// <summary>
        /// Returns the <see cref="T:System.Globalization.DateTimeFormatInfo" /> object associated with
        /// the specified <see cref="T:System.IFormatProvider" />.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Globalization.DateTimeFormatInfo" /> object associated with
        /// <see cref="T:System.IFormatProvider" />.
        /// </returns>
        /// <param name="provider">
        /// The <see cref="T:System.IFormatProvider" /> that gets the
        /// <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.-or- null to get <see cref="P:System.Globalization.DateTimeFormatInfo.CurrentInfo" />.
        /// </param>
        public static System.Globalization.DateTimeFormatInfo GetInstance(System.IFormatProvider provider) { return default(System.Globalization.DateTimeFormatInfo); }
        /// <summary>
        /// Returns the culture-specific full name of the specified month based on the culture associated
        /// with the current <see cref="T:System.Globalization.DateTimeFormatInfo" /> object.
        /// </summary>
        /// <returns>
        /// The culture-specific full name of the month represented by <paramref name="month" />.
        /// </returns>
        /// <param name="month">An integer from 1 through 13 representing the name of the month to retrieve.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="month" /> is less than 1 or greater than 13.
        /// </exception>
        public string GetMonthName(int month) { return default(string); }
        /// <summary>
        /// Returns a read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> wrapper.
        /// </summary>
        /// <returns>
        /// A read-only <see cref="T:System.Globalization.DateTimeFormatInfo" /> wrapper.
        /// </returns>
        /// <param name="dtfi">The <see cref="T:System.Globalization.DateTimeFormatInfo" /> object to wrap.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="dtfi" /> is null.</exception>
        public static System.Globalization.DateTimeFormatInfo ReadOnly(System.Globalization.DateTimeFormatInfo dtfi) { return default(System.Globalization.DateTimeFormatInfo); }
    }
    /// <summary>
    /// Provides culture-specific information for formatting and parsing numeric values.
    /// </summary>
    public sealed partial class NumberFormatInfo : System.IFormatProvider
    {
        /// <summary>
        /// Initializes a new writable instance of the <see cref="T:System.Globalization.NumberFormatInfo" />
        /// class that is culture-independent (invariant).
        /// </summary>
        public NumberFormatInfo() { }
        /// <summary>
        /// Gets or sets the number of decimal places to use in currency values.
        /// </summary>
        /// <returns>
        /// The number of decimal places to use in currency values. The default for
        /// <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 2.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// The property is being set to a value that is less than 0 or greater than 99.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public int CurrencyDecimalDigits { get { return default(int); } set { } }
        /// <summary>
        /// Gets or sets the string to use as the decimal separator in currency values.
        /// </summary>
        /// <returns>
        /// The string to use as the decimal separator in currency values. The default for
        /// <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ".".
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">The property is being set to an empty string.</exception>
        public string CurrencyDecimalSeparator { get { return default(string); } set { } }
        /// <summary>
        /// Gets or sets the string that separates groups of digits to the left of the decimal in currency
        /// values.
        /// </summary>
        /// <returns>
        /// The string that separates groups of digits to the left of the decimal in currency values.
        /// The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ",".
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public string CurrencyGroupSeparator { get { return default(string); } set { } }
        /// <summary>
        /// Gets or sets the number of digits in each group to the left of the decimal in currency values.
        /// </summary>
        /// <returns>
        /// The number of digits in each group to the left of the decimal in currency values. The default
        /// for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is a one-dimensional
        /// array with only one element, which is set to 3.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// The property is being set and the array contains an entry that is less than 0 or greater than
        /// 9.-or- The property is being set and the array contains an entry, other than the last entry, that
        /// is set to 0.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public int[] CurrencyGroupSizes { get { return default(int[]); } set { } }
        /// <summary>
        /// Gets or sets the format pattern for negative currency values.
        /// </summary>
        /// <returns>
        /// The format pattern for negative currency values. The default for
        /// <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "($n)", where "$" is the
        /// <see cref="P:System.Globalization.NumberFormatInfo.CurrencySymbol" /> and <paramref name="n" /> is a number.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// The property is being set to a value that is less than 0 or greater than 15.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public int CurrencyNegativePattern { get { return default(int); } set { } }
        /// <summary>
        /// Gets or sets the format pattern for positive currency values.
        /// </summary>
        /// <returns>
        /// The format pattern for positive currency values. The default for
        /// <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "$n", where "$" is the
        /// <see cref="P:System.Globalization.NumberFormatInfo.CurrencySymbol" /> and <paramref name="n" /> is a number.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// The property is being set to a value that is less than 0 or greater than 3.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public int CurrencyPositivePattern { get { return default(int); } set { } }
        /// <summary>
        /// Gets or sets the string to use as the currency symbol.
        /// </summary>
        /// <returns>
        /// The string to use as the currency symbol. The default for
        /// <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "¤".
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public string CurrencySymbol { get { return default(string); } set { } }
        /// <summary>
        /// Gets a read-only <see cref="T:System.Globalization.NumberFormatInfo" /> that formats values
        /// based on the current culture.
        /// </summary>
        /// <returns>
        /// A read-only <see cref="T:System.Globalization.NumberFormatInfo" /> based on the culture of
        /// the current thread.
        /// </returns>
        public static System.Globalization.NumberFormatInfo CurrentInfo { get { return default(System.Globalization.NumberFormatInfo); } }
        /// <summary>
        /// Gets a read-only <see cref="T:System.Globalization.NumberFormatInfo" /> object that is culture-independent
        /// (invariant).
        /// </summary>
        /// <returns>
        /// A read-only  object that is culture-independent (invariant).
        /// </returns>
        public static System.Globalization.NumberFormatInfo InvariantInfo { get { return default(System.Globalization.NumberFormatInfo); } }
        /// <summary>
        /// Gets a value that indicates whether this <see cref="T:System.Globalization.NumberFormatInfo" />
        /// object is read-only.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only; otherwise,
        /// false.
        /// </returns>
        public bool IsReadOnly { get { return default(bool); } }
        /// <summary>
        /// Gets or sets the string that represents the IEEE NaN (not a number) value.
        /// </summary>
        /// <returns>
        /// The string that represents the IEEE NaN (not a number) value. The default for
        /// <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "NaN".
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public string NaNSymbol { get { return default(string); } set { } }
        /// <summary>
        /// Gets or sets the string that represents negative infinity.
        /// </summary>
        /// <returns>
        /// The string that represents negative infinity. The default for
        /// <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "-Infinity".
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public string NegativeInfinitySymbol { get { return default(string); } set { } }
        /// <summary>
        /// Gets or sets the string that denotes that the associated number is negative.
        /// </summary>
        /// <returns>
        /// The string that denotes that the associated number is negative. The default for
        /// <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "-".
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public string NegativeSign { get { return default(string); } set { } }
        /// <summary>
        /// Gets or sets the number of decimal places to use in numeric values.
        /// </summary>
        /// <returns>
        /// The number of decimal places to use in numeric values. The default for
        /// <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 2.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// The property is being set to a value that is less than 0 or greater than 99.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public int NumberDecimalDigits { get { return default(int); } set { } }
        /// <summary>
        /// Gets or sets the string to use as the decimal separator in numeric values.
        /// </summary>
        /// <returns>
        /// The string to use as the decimal separator in numeric values. The default for
        /// <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ".".
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">The property is being set to an empty string.</exception>
        public string NumberDecimalSeparator { get { return default(string); } set { } }
        /// <summary>
        /// Gets or sets the string that separates groups of digits to the left of the decimal in numeric
        /// values.
        /// </summary>
        /// <returns>
        /// The string that separates groups of digits to the left of the decimal in numeric values. The
        /// default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ",".
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public string NumberGroupSeparator { get { return default(string); } set { } }
        /// <summary>
        /// Gets or sets the number of digits in each group to the left of the decimal in numeric values.
        /// </summary>
        /// <returns>
        /// The number of digits in each group to the left of the decimal in numeric values. The default
        /// for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is a one-dimensional
        /// array with only one element, which is set to 3.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// The property is being set and the array contains an entry that is less than 0 or greater than
        /// 9.-or- The property is being set and the array contains an entry, other than the last entry, that
        /// is set to 0.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public int[] NumberGroupSizes { get { return default(int[]); } set { } }
        /// <summary>
        /// Gets or sets the format pattern for negative numeric values.
        /// </summary>
        /// <returns>
        /// The format pattern for negative numeric values.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// The property is being set to a value that is less than 0 or greater than 4.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public int NumberNegativePattern { get { return default(int); } set { } }
        /// <summary>
        /// Gets or sets the number of decimal places to use in percent values.
        /// </summary>
        /// <returns>
        /// The number of decimal places to use in percent values. The default for
        /// <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 2.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// The property is being set to a value that is less than 0 or greater than 99.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public int PercentDecimalDigits { get { return default(int); } set { } }
        /// <summary>
        /// Gets or sets the string to use as the decimal separator in percent values.
        /// </summary>
        /// <returns>
        /// The string to use as the decimal separator in percent values. The default for
        /// <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ".".
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">The property is being set to an empty string.</exception>
        public string PercentDecimalSeparator { get { return default(string); } set { } }
        /// <summary>
        /// Gets or sets the string that separates groups of digits to the left of the decimal in percent
        /// values.
        /// </summary>
        /// <returns>
        /// The string that separates groups of digits to the left of the decimal in percent values. The
        /// default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ",".
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public string PercentGroupSeparator { get { return default(string); } set { } }
        /// <summary>
        /// Gets or sets the number of digits in each group to the left of the decimal in percent values.
        /// </summary>
        /// <returns>
        /// The number of digits in each group to the left of the decimal in percent values. The default
        /// for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is a one-dimensional
        /// array with only one element, which is set to 3.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// The property is being set and the array contains an entry that is less than 0 or greater than
        /// 9.-or- The property is being set and the array contains an entry, other than the last entry, that
        /// is set to 0.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public int[] PercentGroupSizes { get { return default(int[]); } set { } }
        /// <summary>
        /// Gets or sets the format pattern for negative percent values.
        /// </summary>
        /// <returns>
        /// The format pattern for negative percent values. The default for
        /// <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "-n %", where "%" is the
        /// <see cref="P:System.Globalization.NumberFormatInfo.PercentSymbol" /> and <paramref name="n" /> is a number.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// The property is being set to a value that is less than 0 or greater than 11.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public int PercentNegativePattern { get { return default(int); } set { } }
        /// <summary>
        /// Gets or sets the format pattern for positive percent values.
        /// </summary>
        /// <returns>
        /// The format pattern for positive percent values. The default for
        /// <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "n %", where "%" is the
        /// <see cref="P:System.Globalization.NumberFormatInfo.PercentSymbol" /> and <paramref name="n" /> is a number.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// The property is being set to a value that is less than 0 or greater than 3.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public int PercentPositivePattern { get { return default(int); } set { } }
        /// <summary>
        /// Gets or sets the string to use as the percent symbol.
        /// </summary>
        /// <returns>
        /// The string to use as the percent symbol. The default for
        /// <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "%".
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public string PercentSymbol { get { return default(string); } set { } }
        /// <summary>
        /// Gets or sets the string to use as the per mille symbol.
        /// </summary>
        /// <returns>
        /// The string to use as the per mille symbol. The default for
        /// <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "‰", which is the Unicode character U+2030.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public string PerMilleSymbol { get { return default(string); } set { } }
        /// <summary>
        /// Gets or sets the string that represents positive infinity.
        /// </summary>
        /// <returns>
        /// The string that represents positive infinity. The default for
        /// <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "Infinity".
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The property is being set to null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public string PositiveInfinitySymbol { get { return default(string); } set { } }
        /// <summary>
        /// Gets or sets the string that denotes that the associated number is positive.
        /// </summary>
        /// <returns>
        /// The string that denotes that the associated number is positive. The default for
        /// <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "+".
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">In a set operation, the value to be assigned is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object
        /// is read-only.
        /// </exception>
        public string PositiveSign { get { return default(string); } set { } }
        /// <summary>
        /// Creates a shallow copy of the <see cref="T:System.Globalization.NumberFormatInfo" /> object.
        /// </summary>
        /// <returns>
        /// A new object copied from the original <see cref="T:System.Globalization.NumberFormatInfo" />
        /// object.
        /// </returns>
        public object Clone() { return default(object); }
        /// <summary>
        /// Gets an object of the specified type that provides a number formatting service.
        /// </summary>
        /// <returns>
        /// The current <see cref="T:System.Globalization.NumberFormatInfo" />, if <paramref name="formatType" />
        /// is the same as the type of the current <see cref="T:System.Globalization.NumberFormatInfo" />
        /// ; otherwise, null.
        /// </returns>
        /// <param name="formatType">The <see cref="T:System.Type" /> of the required formatting service.</param>
        public object GetFormat(System.Type formatType) { return default(object); }
        /// <summary>
        /// Gets the <see cref="T:System.Globalization.NumberFormatInfo" /> associated with the specified
        /// <see cref="T:System.IFormatProvider" />.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Globalization.NumberFormatInfo" /> associated with the specified
        /// <see cref="T:System.IFormatProvider" />.
        /// </returns>
        /// <param name="formatProvider">
        /// The <see cref="T:System.IFormatProvider" /> used to get the
        /// <see cref="T:System.Globalization.NumberFormatInfo" />.-or- null to get <see cref="P:System.Globalization.NumberFormatInfo.CurrentInfo" />.
        /// </param>
        public static System.Globalization.NumberFormatInfo GetInstance(System.IFormatProvider formatProvider) { return default(System.Globalization.NumberFormatInfo); }
        /// <summary>
        /// Returns a read-only <see cref="T:System.Globalization.NumberFormatInfo" /> wrapper.
        /// </summary>
        /// <returns>
        /// A read-only <see cref="T:System.Globalization.NumberFormatInfo" /> wrapper around <paramref name="nfi" />.
        /// </returns>
        /// <param name="nfi">The <see cref="T:System.Globalization.NumberFormatInfo" /> to wrap.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="nfi" /> is null.</exception>
        public static System.Globalization.NumberFormatInfo ReadOnly(System.Globalization.NumberFormatInfo nfi) { return default(System.Globalization.NumberFormatInfo); }
    }
    /// <summary>
    /// Contains information about the country/region.
    /// </summary>
    public partial class RegionInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Globalization.RegionInfo" /> class based
        /// on the country/region or specific culture, specified by name.
        /// </summary>
        /// <param name="name">
        /// A string that contains a two-letter code defined in ISO 3166 for country/region.-or-A string
        /// that contains the culture name for a specific culture, custom culture, or Windows-only culture. If
        /// the culture name is not in RFC 4646 format, your application should specify the entire culture name
        /// instead of just the country/region.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="name" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="name" /> is not a valid country/region name or specific culture name.
        /// </exception>
        public RegionInfo(string name) { }
        /// <summary>
        /// Gets the currency symbol associated with the country/region.
        /// </summary>
        /// <returns>
        /// The currency symbol associated with the country/region.
        /// </returns>
        public virtual string CurrencySymbol { get { return default(string); } }
        /// <summary>
        /// Gets the <see cref="T:System.Globalization.RegionInfo" /> that represents the country/region
        /// used by the current thread.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Globalization.RegionInfo" /> that represents the country/region used
        /// by the current thread.
        /// </returns>
        public static System.Globalization.RegionInfo CurrentRegion { get { return default(System.Globalization.RegionInfo); } }
        /// <summary>
        /// Gets the full name of the country/region in the language of the localized version of .NET Framework.
        /// </summary>
        /// <returns>
        /// The full name of the country/region in the language of the localized version of .NET Framework.
        /// </returns>
        public virtual string DisplayName { get { return default(string); } }
        /// <summary>
        /// Gets the full name of the country/region in English.
        /// </summary>
        /// <returns>
        /// The full name of the country/region in English.
        /// </returns>
        public virtual string EnglishName { get { return default(string); } }
        /// <summary>
        /// Gets a value indicating whether the country/region uses the metric system for measurements.
        /// </summary>
        /// <returns>
        /// true if the country/region uses the metric system for measurements; otherwise, false.
        /// </returns>
        public virtual bool IsMetric { get { return default(bool); } }
        /// <summary>
        /// Gets the three-character ISO 4217 currency symbol associated with the country/region.
        /// </summary>
        /// <returns>
        /// The three-character ISO 4217 currency symbol associated with the country/region.
        /// </returns>
        public virtual string ISOCurrencySymbol { get { return default(string); } }
        /// <summary>
        /// Gets the name or ISO 3166 two-letter country/region code for the current
        /// <see cref="T:System.Globalization.RegionInfo" /> object.
        /// </summary>
        /// <returns>
        /// The value specified by the <paramref name="name" /> parameter of the
        /// <see cref="M:System.Globalization.RegionInfo.#ctor(System.String)" /> constructor. The return value is in uppercase.-or-The two-letter code defined in ISO 3166
        /// for the country/region specified by the <paramref name="culture" /> parameter of the
        /// <see cref="M:System.Globalization.RegionInfo.#ctor(System.Int32)" /> constructor. The return value
        /// is in uppercase.
        /// </returns>
        public virtual string Name { get { return default(string); } }
        /// <summary>
        /// Gets the name of a country/region formatted in the native language of the country/region.
        /// </summary>
        /// <returns>
        /// The native name of the country/region formatted in the language associated with the ISO 3166
        /// country/region code.
        /// </returns>
        public virtual string NativeName { get { return default(string); } }
        /// <summary>
        /// Gets the two-letter code defined in ISO 3166 for the country/region.
        /// </summary>
        /// <returns>
        /// The two-letter code defined in ISO 3166 for the country/region.
        /// </returns>
        public virtual string TwoLetterISORegionName { get { return default(string); } }
        /// <summary>
        /// Determines whether the specified object is the same instance as the current
        /// <see cref="T:System.Globalization.RegionInfo" />.
        /// </summary>
        /// <returns>
        /// true if the <paramref name="value" /> parameter is a <see cref="T:System.Globalization.RegionInfo" />
        /// object and its <see cref="P:System.Globalization.RegionInfo.Name" /> property is the same
        /// as the <see cref="P:System.Globalization.RegionInfo.Name" /> property of the current
        /// <see cref="T:System.Globalization.RegionInfo" /> object; otherwise, false.
        /// </returns>
        /// <param name="value">The object to compare with the current <see cref="T:System.Globalization.RegionInfo" />.</param>
        public override bool Equals(object value) { return default(bool); }
        /// <summary>
        /// Serves as a hash function for the current <see cref="T:System.Globalization.RegionInfo" />,
        /// suitable for hashing algorithms and data structures, such as a hash table.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Globalization.RegionInfo" />.
        /// </returns>
        public override int GetHashCode() { return default(int); }
        /// <summary>
        /// Returns a string containing the culture name or ISO 3166 two-letter country/region codes specified
        /// for the current <see cref="T:System.Globalization.RegionInfo" />.
        /// </summary>
        /// <returns>
        /// A string containing the culture name or ISO 3166 two-letter country/region codes defined for
        /// the current <see cref="T:System.Globalization.RegionInfo" />.
        /// </returns>
        public override string ToString() { return default(string); }
    }
    /// <summary>
    /// Provides functionality to split a string into text elements and to iterate through those text
    /// elements.
    /// </summary>
    public partial class StringInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Globalization.StringInfo" /> class.
        /// </summary>
        public StringInfo() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Globalization.StringInfo" /> class to
        /// a specified string.
        /// </summary>
        /// <param name="value">A string to initialize this <see cref="T:System.Globalization.StringInfo" /> object.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="value" /> is null.</exception>
        public StringInfo(string value) { }
        /// <summary>
        /// Gets the number of text elements in the current <see cref="T:System.Globalization.StringInfo" />
        /// object.
        /// </summary>
        /// <returns>
        /// The number of base characters, surrogate pairs, and combining character sequences in this
        /// <see cref="T:System.Globalization.StringInfo" /> object.
        /// </returns>
        public int LengthInTextElements { get { return default(int); } }
        /// <summary>
        /// Gets or sets the value of the current <see cref="T:System.Globalization.StringInfo" /> object.
        /// </summary>
        /// <returns>
        /// The string that is the value of the current <see cref="T:System.Globalization.StringInfo" />
        /// object.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The value in a set operation is null.</exception>
        public string String { get { return default(string); } set { } }
        /// <summary>
        /// Indicates whether the current <see cref="T:System.Globalization.StringInfo" /> object is equal
        /// to a specified object.
        /// </summary>
        /// <returns>
        /// true if the <paramref name="value" /> parameter is a <see cref="T:System.Globalization.StringInfo" />
        /// object and its <see cref="P:System.Globalization.StringInfo.String" /> property equals
        /// the <see cref="P:System.Globalization.StringInfo.String" /> property of this
        /// <see cref="T:System.Globalization.StringInfo" /> object; otherwise, false.
        /// </returns>
        /// <param name="value">An object.</param>
        public override bool Equals(object value) { return default(bool); }
        /// <summary>
        /// Calculates a hash code for the value of the current <see cref="T:System.Globalization.StringInfo" />
        /// object.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer hash code based on the string value of this
        /// <see cref="T:System.Globalization.StringInfo" /> object.
        /// </returns>
        public override int GetHashCode() { return default(int); }
        /// <summary>
        /// Gets the first text element in a specified string.
        /// </summary>
        /// <returns>
        /// A string containing the first text element in the specified string.
        /// </returns>
        /// <param name="str">The string from which to get the text element.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="str" /> is null.</exception>
        public static string GetNextTextElement(string str) { return default(string); }
        /// <summary>
        /// Gets the text element at the specified index of the specified string.
        /// </summary>
        /// <returns>
        /// A string containing the text element at the specified index of the specified string.
        /// </returns>
        /// <param name="str">The string from which to get the text element.</param>
        /// <param name="index">The zero-based index at which the text element starts.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="str" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index" /> is outside the range of valid indexes for <paramref name="str" />.
        /// </exception>
        public static string GetNextTextElement(string str, int index) { return default(string); }
        /// <summary>
        /// Returns an enumerator that iterates through the text elements of the entire string.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Globalization.TextElementEnumerator" /> for the entire string.
        /// </returns>
        /// <param name="str">The string to iterate through.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="str" /> is null.</exception>
        public static System.Globalization.TextElementEnumerator GetTextElementEnumerator(string str) { return default(System.Globalization.TextElementEnumerator); }
        /// <summary>
        /// Returns an enumerator that iterates through the text elements of the string, starting at the
        /// specified index.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Globalization.TextElementEnumerator" /> for the string starting at
        /// <paramref name="index" />.
        /// </returns>
        /// <param name="str">The string to iterate through.</param>
        /// <param name="index">The zero-based index at which to start iterating.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="str" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index" /> is outside the range of valid indexes for <paramref name="str" />.
        /// </exception>
        public static System.Globalization.TextElementEnumerator GetTextElementEnumerator(string str, int index) { return default(System.Globalization.TextElementEnumerator); }
        /// <summary>
        /// Returns the indexes of each base character, high surrogate, or control character within the
        /// specified string.
        /// </summary>
        /// <returns>
        /// An array of integers that contains the zero-based indexes of each base character, high surrogate,
        /// or control character within the specified string.
        /// </returns>
        /// <param name="str">The string to search.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="str" /> is null.</exception>
        public static int[] ParseCombiningCharacters(string str) { return default(int[]); }
    }
    /// <summary>
    /// Enumerates the text elements of a string.
    /// </summary>
    public partial class TextElementEnumerator : System.Collections.IEnumerator
    {
        internal TextElementEnumerator() { }
        /// <summary>
        /// Gets the current text element in the string.
        /// </summary>
        /// <returns>
        /// An object containing the current text element in the string.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">
        /// The enumerator is positioned before the first text element of the string or after the last
        /// text element.
        /// </exception>
        public object Current { get { return default(object); } }
        /// <summary>
        /// Gets the index of the text element that the enumerator is currently positioned over.
        /// </summary>
        /// <returns>
        /// The index of the text element that the enumerator is currently positioned over.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">
        /// The enumerator is positioned before the first text element of the string or after the last
        /// text element.
        /// </exception>
        public int ElementIndex { get { return default(int); } }
        /// <summary>
        /// Gets the current text element in the string.
        /// </summary>
        /// <returns>
        /// A new string containing the current text element in the string being read.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">
        /// The enumerator is positioned before the first text element of the string or after the last
        /// text element.
        /// </exception>
        public string GetTextElement() { return default(string); }
        /// <summary>
        /// Advances the enumerator to the next text element of the string.
        /// </summary>
        /// <returns>
        /// true if the enumerator was successfully advanced to the next text element; false if the enumerator
        /// has passed the end of the string.
        /// </returns>
        public bool MoveNext() { return default(bool); }
        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first text element in the
        /// string.
        /// </summary>
        public void Reset() { }
    }
    /// <summary>
    /// Defines text properties and behaviors, such as casing, that are specific to a writing system.
    /// </summary>
    public partial class TextInfo
    {
        internal TextInfo() { }
        /// <summary>
        /// Gets the name of the culture associated with the current <see cref="T:System.Globalization.TextInfo" />
        /// object.
        /// </summary>
        /// <returns>
        /// The name of a culture.
        /// </returns>
        public string CultureName { get { return default(string); } }
        /// <summary>
        /// Gets a value indicating whether the current <see cref="T:System.Globalization.TextInfo" />
        /// object is read-only.
        /// </summary>
        /// <returns>
        /// true if the current <see cref="T:System.Globalization.TextInfo" /> object is read-only; otherwise,
        /// false.
        /// </returns>
        public bool IsReadOnly { get { return default(bool); } }
        /// <summary>
        /// Gets a value indicating whether the current <see cref="T:System.Globalization.TextInfo" />
        /// object represents a writing system where text flows from right to left.
        /// </summary>
        /// <returns>
        /// true if text flows from right to left; otherwise, false.
        /// </returns>
        public bool IsRightToLeft { get { return default(bool); } }
        /// <summary>
        /// Gets or sets the string that separates items in a list.
        /// </summary>
        /// <returns>
        /// The string that separates items in a list.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The value in a set operation is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// In a set operation, the current <see cref="T:System.Globalization.TextInfo" /> object is read-only.
        /// </exception>
        public virtual string ListSeparator { get { return default(string); } set { } }
        /// <summary>
        /// Determines whether the specified object represents the same writing system as the current
        /// <see cref="T:System.Globalization.TextInfo" /> object.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj" /> represents the same writing system as the current
        /// <see cref="T:System.Globalization.TextInfo" />; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current <see cref="T:System.Globalization.TextInfo" />.</param>
        public override bool Equals(object obj) { return default(bool); }
        /// <summary>
        /// Serves as a hash function for the current <see cref="T:System.Globalization.TextInfo" />,
        /// suitable for hashing algorithms and data structures, such as a hash table.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Globalization.TextInfo" />.
        /// </returns>
        public override int GetHashCode() { return default(int); }
        /// <summary>
        /// Converts the specified character to lowercase.
        /// </summary>
        /// <returns>
        /// The specified character converted to lowercase.
        /// </returns>
        /// <param name="c">The character to convert to lowercase.</param>
        public virtual char ToLower(char c) { return default(char); }
        /// <summary>
        /// Converts the specified string to lowercase.
        /// </summary>
        /// <returns>
        /// The specified string converted to lowercase.
        /// </returns>
        /// <param name="str">The string to convert to lowercase.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="str" /> is null.</exception>
        public virtual string ToLower(string str) { return default(string); }
        /// <summary>
        /// Returns a string that represents the current <see cref="T:System.Globalization.TextInfo" />.
        /// </summary>
        /// <returns>
        /// A string that represents the current <see cref="T:System.Globalization.TextInfo" />.
        /// </returns>
        public override string ToString() { return default(string); }
        /// <summary>
        /// Converts the specified character to uppercase.
        /// </summary>
        /// <returns>
        /// The specified character converted to uppercase.
        /// </returns>
        /// <param name="c">The character to convert to uppercase.</param>
        public virtual char ToUpper(char c) { return default(char); }
        /// <summary>
        /// Converts the specified string to uppercase.
        /// </summary>
        /// <returns>
        /// The specified string converted to uppercase.
        /// </returns>
        /// <param name="str">The string to convert to uppercase.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="str" /> is null.</exception>
        public virtual string ToUpper(string str) { return default(string); }
    }
    /// <summary>
    /// Defines the Unicode category of a character.
    /// </summary>
    public enum UnicodeCategory
    {
        /// <summary>
        /// Closing character of one of the paired punctuation marks, such as parentheses, square brackets,
        /// and braces. Signified by the Unicode designation "Pe" (punctuation, close). The value is 21.
        /// </summary>
        ClosePunctuation = 21,
        /// <summary>
        /// Connector punctuation character that connects two characters. Signified by the Unicode designation
        /// "Pc" (punctuation, connector). The value is 18.
        /// </summary>
        ConnectorPunctuation = 18,
        /// <summary>
        /// Control code character, with a Unicode value of U+007F or in the range U+0000 through U+001F
        /// or U+0080 through U+009F. Signified by the Unicode designation "Cc" (other, control). The value is
        /// 14.
        /// </summary>
        Control = 14,
        /// <summary>
        /// Currency symbol character. Signified by the Unicode designation "Sc" (symbol, currency). The
        /// value is 26.
        /// </summary>
        CurrencySymbol = 26,
        /// <summary>
        /// Dash or hyphen character. Signified by the Unicode designation "Pd" (punctuation, dash). The
        /// value is 19.
        /// </summary>
        DashPunctuation = 19,
        /// <summary>
        /// Decimal digit character, that is, a character in the range 0 through 9. Signified by the Unicode
        /// designation "Nd" (number, decimal digit). The value is 8.
        /// </summary>
        DecimalDigitNumber = 8,
        /// <summary>
        /// Enclosing mark character, which is a nonspacing combining character that surrounds all previous
        /// characters up to and including a base character. Signified by the Unicode designation "Me" (mark,
        /// enclosing). The value is 7.
        /// </summary>
        EnclosingMark = 7,
        /// <summary>
        /// Closing or final quotation mark character. Signified by the Unicode designation "Pf" (punctuation,
        /// final quote). The value is 23.
        /// </summary>
        FinalQuotePunctuation = 23,
        /// <summary>
        /// Format character that affects the layout of text or the operation of text processes, but is
        /// not normally rendered. Signified by the Unicode designation "Cf" (other, format). The value is 15.
        /// </summary>
        Format = 15,
        /// <summary>
        /// Opening or initial quotation mark character. Signified by the Unicode designation "Pi" (punctuation,
        /// initial quote). The value is 22.
        /// </summary>
        InitialQuotePunctuation = 22,
        /// <summary>
        /// Number represented by a letter, instead of a decimal digit, for example, the Roman numeral
        /// for five, which is "V". The indicator is signified by the Unicode designation "Nl" (number, letter).
        /// The value is 9.
        /// </summary>
        LetterNumber = 9,
        /// <summary>
        /// Character that is used to separate lines of text. Signified by the Unicode designation "Zl"
        /// (separator, line). The value is 12.
        /// </summary>
        LineSeparator = 12,
        /// <summary>
        /// Lowercase letter. Signified by the Unicode designation "Ll" (letter, lowercase). The value
        /// is 1.
        /// </summary>
        LowercaseLetter = 1,
        /// <summary>
        /// Mathematical symbol character, such as "+" or "= ". Signified by the Unicode designation "Sm"
        /// (symbol, math). The value is 25.
        /// </summary>
        MathSymbol = 25,
        /// <summary>
        /// Modifier letter character, which is free-standing spacing character that indicates modifications
        /// of a preceding letter. Signified by the Unicode designation "Lm" (letter, modifier). The value
        /// is 3.
        /// </summary>
        ModifierLetter = 3,
        /// <summary>
        /// Modifier symbol character, which indicates modifications of surrounding characters. For example,
        /// the fraction slash indicates that the number to the left is the numerator and the number to the
        /// right is the denominator. The indicator is signified by the Unicode designation "Sk" (symbol, modifier).
        /// The value is 27.
        /// </summary>
        ModifierSymbol = 27,
        /// <summary>
        /// Nonspacing character that indicates modifications of a base character. Signified by the Unicode
        /// designation "Mn" (mark, nonspacing). The value is 5.
        /// </summary>
        NonSpacingMark = 5,
        /// <summary>
        /// Opening character of one of the paired punctuation marks, such as parentheses, square brackets,
        /// and braces. Signified by the Unicode designation "Ps" (punctuation, open). The value is 20.
        /// </summary>
        OpenPunctuation = 20,
        /// <summary>
        /// Letter that is not an uppercase letter, a lowercase letter, a titlecase letter, or a modifier
        /// letter. Signified by the Unicode designation "Lo" (letter, other). The value is 4.
        /// </summary>
        OtherLetter = 4,
        /// <summary>
        /// Character that is not assigned to any Unicode category. Signified by the Unicode designation
        /// "Cn" (other, not assigned). The value is 29.
        /// </summary>
        OtherNotAssigned = 29,
        /// <summary>
        /// Number that is neither a decimal digit nor a letter number, for example, the fraction 1/2.
        /// The indicator is signified by the Unicode designation "No" (number, other). The value is 10.
        /// </summary>
        OtherNumber = 10,
        /// <summary>
        /// Punctuation character that is not a connector, a dash, open punctuation, close punctuation,
        /// an initial quote, or a final quote. Signified by the Unicode designation "Po" (punctuation, other).
        /// The value is 24.
        /// </summary>
        OtherPunctuation = 24,
        /// <summary>
        /// Symbol character that is not a mathematical symbol, a currency symbol or a modifier symbol.
        /// Signified by the Unicode designation "So" (symbol, other). The value is 28.
        /// </summary>
        OtherSymbol = 28,
        /// <summary>
        /// Character used to separate paragraphs. Signified by the Unicode designation "Zp" (separator,
        /// paragraph). The value is 13.
        /// </summary>
        ParagraphSeparator = 13,
        /// <summary>
        /// Private-use character, with a Unicode value in the range U+E000 through U+F8FF. Signified by
        /// the Unicode designation "Co" (other, private use). The value is 17.
        /// </summary>
        PrivateUse = 17,
        /// <summary>
        /// Space character, which has no glyph but is not a control or format character. Signified by
        /// the Unicode designation "Zs" (separator, space). The value is 11.
        /// </summary>
        SpaceSeparator = 11,
        /// <summary>
        /// Spacing character that indicates modifications of a base character and affects the width of
        /// the glyph for that base character. Signified by the Unicode designation "Mc" (mark, spacing combining).
        /// The value is 6.
        /// </summary>
        SpacingCombiningMark = 6,
        /// <summary>
        /// High surrogate or a low surrogate character. Surrogate code values are in the range U+D800
        /// through U+DFFF. Signified by the Unicode designation "Cs" (other, surrogate). The value is 16.
        /// </summary>
        Surrogate = 16,
        /// <summary>
        /// Titlecase letter. Signified by the Unicode designation "Lt" (letter, titlecase). The value
        /// is 2.
        /// </summary>
        TitlecaseLetter = 2,
        /// <summary>
        /// Uppercase letter. Signified by the Unicode designation "Lu" (letter, uppercase). The value
        /// is 0.
        /// </summary>
        UppercaseLetter = 0,
    }
}
