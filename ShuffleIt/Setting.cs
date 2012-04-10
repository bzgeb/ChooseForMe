// Copyright (c) Adam Nathan.  All rights reserved.
//
// By purchasing the book that this source code belongs to, you may use and
// modify this code for commercial and non-commercial applications, but you
// may not publish the source code.
// THE SOURCE CODE IS PROVIDED "AS IS", WITH NO WARRANTIES OR INDEMNITIES.
using System.IO.IsolatedStorage;

namespace ShuffleIt
{
  // Encapsulates a key/value pair stored in Isolated Storage ApplicationSettings
  public class Setting<T>
  {
    string name;
    T value;
    T defaultValue;
    bool hasValue;

    public Setting(string name, T defaultValue)
    {
      this.name = name;
      this.defaultValue = defaultValue;
    }

    public T Value
    {
      get
      {
        // Check for the cached value
        if (!this.hasValue)
        {
          // Try to get the value from Isolated Storage
          if (!IsolatedStorageSettings.ApplicationSettings.TryGetValue(
                this.name, out this.value))
          {
            // It hasn't been set yet
            this.value = this.defaultValue;
            IsolatedStorageSettings.ApplicationSettings[this.name] = this.value;
          }
          this.hasValue = true;
        }

        return this.value;
      }

      set
      {
        // Save the value to Isolated Storage
        IsolatedStorageSettings.ApplicationSettings[this.name] = value;
        this.value = value;
        this.hasValue = true;
      }
    }

    public T DefaultValue
    {
      get { return this.defaultValue; }
    }

    // "Clear" cached value:
    public void ForceRefresh()
    {
      this.hasValue = false;
    }
  }
}