#region License
/*
 * Copyright 2021 AutoCore
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 * Authors: AutoCore Members
 *
 */
#endregion

using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class AddressNode : MonoBehaviour
    {
        public Action<AddressConfig> OnAddressConfigChange;
        private AddressConfig config;
        public AddressConfig Config
        {
            get
            {
                return config;
            }
            set
            {
                config = value;
                OnAddressConfigChange.Invoke(value);
            }
        }
    }

}
