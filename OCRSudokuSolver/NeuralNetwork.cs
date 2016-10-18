using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

// Based on http://neuralnetworksanddeeplearning.com series



namespace OCRSudokuSolver
{

    namespace NeuralNetworkNamespace
    {
        /// <summary>
        /// Neural network library written base on the http://neuralnetworksanddeeplearning.com series.
        /// This version contains the standard backpropagation algorithm with stochastical gradient descent
        /// Plus there are included features from chapter 3 like better weight inicialization, cross-entrophy cost function, L2 regularization
        /// </summary>
        public partial class NeuralNetwork : IXmlSerializable
        {
            /// <summary>
            /// Matrix of weights for all layers
            /// We'll use m_weights[l][j,k] to denote the weight for the connection from the k-th neuron in the (l−1)-th layer to the j-th neuron in the l-th layer. 
            /// </summary>
            private My2DMatrix[] m_weights;
            /// <summary>
            /// <p>Lowest activation values for a neutron to fire </p>
            /// <p>We use m_biases[l][j] for the bias of the j-th neuron in the l-th layer</p>
            /// </summary>
            private MyVector[] m_biases;

            public delegate void Print(String value);
            public delegate bool Cancel();

            private NeuralNetwork()
            {
            }

            /// <summary>
            /// Constructor that generates the neural network
            /// </summary>
            /// <param name="sizes">The list ``sizes`` contains the number of neurons in the respective
            ///  layers of the network.  For example, if the list was [2, 3, 1]
            ///  then it would be a three-layer network, with the first layer
            ///  containing 2 neurons, the second layer 3 neurons, and the
            ///  third layer 1 neuron.</param>
            public NeuralNetwork(int[] sizes)
            {
                //Initialize each weight using a Gaussian distribution with mean 0
                //and standard deviation 1 over the square root of the number of
                //weights connecting to the same neuron.  Initialize the biases
                //using a Gaussian distribution with mean 0 and standard
                //deviation 1.
                //Note that the first layer is assumed to be an input layer, and
                //by convention we won't set any biases for those neurons, since
                //biases are only ever used in computing the outputs from later
                //layers.
                m_biases = new MyVector[sizes.Length];
                m_weights = new My2DMatrix[sizes.Length];
                m_biases[0] = new MyVector(sizes[0]); // Just for the NumOfNeuronsInLayer
                for (int i = 1; i < sizes.Length; i++)
                {
                    m_biases[i] = new MyVector(sizes[i]);
                    m_weights[i] = new My2DMatrix(sizes[i], sizes[i - 1]);
                    for (int j = 0; j < sizes[i]; j++)
                    {
                        m_biases[i][j] = NextGaussianDistribution();
                        for (int k = 0; k < sizes[i - 1]; k++)
                        {
                            m_weights[i][j, k] = NextGaussianDistribution(0, 1.0 / Math.Sqrt(sizes[i - 1]));
                        }
                    }
                }

            }

            public int NumOfLayers { get { return m_biases.Length; } }
            public int NumOfNeuronsInLayer(int index) { return m_biases[index].Length; }

            #region Serialization
            public void Save(string filePath)
            {
                var serializer = new XmlSerializer(typeof(NeuralNetwork));
                var fileStream = new StreamWriter(filePath);
                serializer.Serialize(fileStream, this);
                fileStream.Flush();
                fileStream.Close();
            }

            public static NeuralNetwork FromFile(string filePath)
            {
                var serializer = new XmlSerializer(typeof(NeuralNetwork));
                var fileStream = new StreamReader(filePath);
                NeuralNetwork net = (NeuralNetwork)serializer.Deserialize(fileStream);
                fileStream.Close();
                return net;
            }

            public System.Xml.Schema.XmlSchema GetSchema()
            {
                return null;
            }

            public void ReadXml(System.Xml.XmlReader reader)
            {
                var vectorSerializer = new XmlSerializer(typeof(MyVector));
                var matrixSerializer = new XmlSerializer(typeof(My2DMatrix));
                List<MyVector> vectors = new List<MyVector>();
                List<My2DMatrix> matrices = new List<My2DMatrix>() { null };
                reader.Read(); // start element NeuralNetwork
                reader.Read(); // start element ArrayOfMyVector
                while (reader.Name == "MyVector")
                {
                    vectors.Add((MyVector)vectorSerializer.Deserialize(reader));
                    reader.Read(); // End element MyVector 
                }
                m_biases = vectors.ToArray();
                reader.ReadToFollowing("My2DMatrix");
                while (reader.Name == "My2DMatrix")
                {
                    matrices.Add((My2DMatrix)matrixSerializer.Deserialize(reader));
                    reader.Read(); // End element My2DMatrix 
                }
                m_weights = matrices.ToArray();
            }

            public void WriteXml(System.Xml.XmlWriter writer)
            {
                var vectorSerializer = new XmlSerializer(typeof(MyVector[]));
                var matrixSerializer = new XmlSerializer(typeof(My2DMatrix[]));
                vectorSerializer.Serialize(writer, m_biases);
                matrixSerializer.Serialize(writer, m_weights);
            }

            #endregion

            #region Algorithmic functions
            /// <summary>
            /// Calculates the output of the network
            /// </summary>
            /// <returns>MyVector containing the output of the output layer</returns>
            public MyVector FeedForward(MyVector input)
            {
                Debug.Assert(m_biases.Length == m_weights.Length);
                MyVector result = new MyVector(input);
                for (int i = 1; i < NumOfLayers; i++)
                {
                    Application.DoEvents();
                    result = ((m_weights[i] * result) + m_biases[i]).ApplyFunction(Sigmoid);
                }
                return result;
            }

            private void UpdateMiniBatch(Tuple<MyVector, MyVector>[] miniBatch, double eta, double lambda, int n, Cancel cancelFnc)
            {
                // Represents the sum of the the partial derivvatives of Cost function with respect to bias over the batch
                MyVector[] parcDerivBiases = new MyVector[NumOfLayers];
                for (int i = 1; i < parcDerivBiases.Length; i++)
                {
                    if (cancelFnc != null && cancelFnc())
                        return;
                    parcDerivBiases[i] = new MyVector(NumOfNeuronsInLayer(i));
                }

                // Represents the sum of the the partial derivvatives of Cost function with respect to weights over the batch
                My2DMatrix[] parcDerivWeights = new My2DMatrix[NumOfLayers];
                for (int i = 1; i < parcDerivBiases.Length; i++)
                {
                    if (cancelFnc != null && cancelFnc())
                        return;
                    parcDerivWeights[i] = new My2DMatrix(m_weights[i].RowsCount, m_weights[i].ColumnsCount);
                }

                for (int i = 0; i < miniBatch.Length; i++)
                {
                    // calculate the partial derivatives
                    var delta = Backpropagate(miniBatch[i].Item1, miniBatch[i].Item2, cancelFnc);
                    for (int j = 1; j < NumOfLayers; j++)
                    {
                        if (cancelFnc != null && cancelFnc())
                            return;
                        parcDerivBiases[j] = parcDerivBiases[j] + delta.Item1[j]; // Vector + Vector
                        parcDerivWeights[j] = parcDerivWeights[j] + delta.Item2[j]; // Matrix + Matrix
                    }
                }
                for (int i = 1; i < NumOfLayers; i++)
                {
                    if (cancelFnc != null && cancelFnc())
                        return;
                    m_weights[i] = (1 - eta * (lambda / n)) * m_weights[i] - (eta / miniBatch.Length) * parcDerivWeights[i]; // (const * Matrix) - (const*Matrix)
                    m_biases[i] = m_biases[i] - (eta / miniBatch.Length) * parcDerivBiases[i]; // Vector - (const*Vector)
                }
            }

            private Tuple<MyVector[], My2DMatrix[]> Backpropagate(MyVector input, MyVector diseredOutput, Cancel cancelFnc)
            {
                MyVector[] parcDerivBiases = new MyVector[NumOfLayers];
                My2DMatrix[] parcDerivWeights = new My2DMatrix[NumOfLayers];

                MyVector activation = new MyVector(input);
                List<MyVector> activations = new List<MyVector>(NumOfLayers) { activation };

                // Feedfoward
                List<MyVector> weightedInputs = new List<MyVector>(NumOfLayers);
                for (int i = 1; i < NumOfLayers; i++)
                {
                    if (cancelFnc != null && cancelFnc())
                        return null;
                    MyVector weightedInput = (m_weights[i] * activation) + m_biases[i];
                    weightedInputs.Add(weightedInput);
                    activation = weightedInput.ApplyFunction(Sigmoid);
                    activations.Add(activation);
                }

                // Backpropagate
                MyVector delta = CrossEntrophyDelta(activations[activations.Count - 1], diseredOutput);
                parcDerivBiases[parcDerivBiases.Length - 1] = delta;
                parcDerivWeights[parcDerivWeights.Length - 1] = new My2DMatrix(delta, activations[activations.Count - 2]);
                for (int l = 2; l < NumOfLayers; l++)
                {
                    if (cancelFnc != null && cancelFnc())
                        return null;
                    MyVector weightedInput = weightedInputs[weightedInputs.Count - l];
                    var spv = weightedInput.ApplyFunction(SigmoidPrime);
                    delta = (m_weights[m_weights.Length - l + 1].Transpose() * delta) * spv;
                    parcDerivBiases[parcDerivBiases.Length - l] = delta; // the error
                    parcDerivWeights[parcDerivWeights.Length - l] = new My2DMatrix(delta, activations[activations.Count - l - 1]); // error times the activation from the previous layer (vector times vector^T creates  matrix)
                }
                return new Tuple<MyVector[], My2DMatrix[]>(parcDerivBiases, parcDerivWeights);
            }

            public void StochasticGradientDescent(Tuple<MyVector, MyVector>[] trainData, int numOfEpochs, int miniBatchSize,
                double eta, Cancel cancelFnc, Print printFnc = null, String outputPath = "", double lambda = 0.0, Tuple<MyVector, MyVector>[] evalData = null,
                bool monitorEvaluationCost = false, bool monitorEvaluationAccuracy = false, bool monitorTrainingCost = false, bool monitorTrainingAccuracy = false)
            {
                for (int i = 0; i < numOfEpochs; i++)
                {
                    if (cancelFnc != null && cancelFnc())
                        return;
                    ShuffleList(trainData, cancelFnc);

                    // Create batches of training data
                    int numOfBatches = trainData.Length / miniBatchSize;
                    List<Tuple<MyVector, MyVector>[]> miniBatches = new List<Tuple<MyVector, MyVector>[]>(numOfBatches);
                    for (int k = 0; k < numOfBatches; k++)
                    {
                        if (cancelFnc != null && cancelFnc())
                            return;
                        Tuple<MyVector, MyVector>[] tempBatch = new Tuple<MyVector, MyVector>[miniBatchSize];
                        for (int j = 0; j < miniBatchSize; j++)
                        {
                            if (cancelFnc != null && cancelFnc())
                                return;
                            tempBatch[j] = trainData[k * miniBatchSize + j];
                        }
                        miniBatches.Add(tempBatch);
                    }

                    // Update network for each minibatch
                    for (int j = 0; j < numOfBatches; j++)
                    {
                        UpdateMiniBatch(miniBatches[j], eta, lambda, trainData.Length, cancelFnc);
                    }
                    if (cancelFnc != null && cancelFnc())
                        return;
                    Debug.Print("Epoch {0} training complete", i);
                    printFnc(String.Format("Epoch {0} training complete", i));
                    this.Save(Path.Combine(outputPath, "netInEpoch" + i + ".xml"));

                    if (monitorTrainingAccuracy)
                    {
                        var accuracy = Accuracy(trainData, cancelFnc);
                        if (cancelFnc != null && cancelFnc())
                            return;
                        if (printFnc != null)
                            printFnc(String.Format("Epoch {2} - {0}/{1}, accuracy on training data", accuracy, trainData.Length, i));
                        Debug.Print("Epoch {2} - {0}/{1}, accuracy on training data", accuracy, trainData.Length, i);
                    }
                    if (monitorEvaluationAccuracy)
                    {
                        var accuracy = Accuracy(evalData, cancelFnc);
                        if (cancelFnc != null && cancelFnc())
                            return;
                        if (printFnc != null)
                            printFnc(String.Format("Epoch {2} - {0}/{1}, accuracy on eval data", accuracy, evalData.Length, i));
                        Debug.Print("Epoch {2} - {0}/{1}, accuracy on eval data", accuracy, evalData.Length, i);
                    }
                }
            }

            #endregion

            #region Math Functions

            /// <summary>
            /// Continuous version of sign function
            /// </summary>
            /// <param name="z"></param>
            /// <returns></returns>
            private static double Sigmoid(double z)
            {
                return 1.0 / (1.0 + Math.Exp(-z));
            }

            /// <summary>
            /// First derivative of Sigmoid function
            /// </summary>
            /// <param name="z"></param>
            /// <returns></returns>
            private static double SigmoidPrime(double z)
            {
                return Sigmoid(z) * (1 - Sigmoid(z));
            }

            private static double CrossEntrophyFnc(MyVector a, MyVector y)
            {
                double result = 0.0;
                for (int i = 0; i < a.Length; i++)
                {
                    result += (-y[i] * Math.Log(a[i]) - (1 - y[i]) * Math.Log(1 - a[i]));
                }
                return result;
            }

            private static MyVector CrossEntrophyDelta(MyVector a, MyVector y)
            {
                return (a - y);
            }

            #endregion

            #region Monitoring functions

            /// <summary>
            /// Returns how many of samples from data are correct
            /// </summary>
            /// <param name="data"></param>
            /// <param name="cancelFnc"></param>
            /// <returns></returns>
            private int Accuracy(Tuple<MyVector, MyVector>[] data, Cancel cancelFnc)
            {
                int numOfCorrect = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    if (cancelFnc != null && cancelFnc())
                        return 0;
                    var output = FeedForward(data[i].Item1);
                    int result = 0;
                    int desiredResult = 0;
                    for (int j = 0; j < output.Length; j++)
                    {
                        if (cancelFnc != null && cancelFnc())
                            return 0;
                        if (output[j] > output[result])
                            result = j;
                        if (data[i].Item2[j] > output[desiredResult])
                            desiredResult = j;
                    }
                    if (desiredResult == result)
                        numOfCorrect++;
                }
                return numOfCorrect;
            }

            //public int TotalCost(Tuple<MyVector, MyVector>[] data, double lambda)
            //{
            //    double cost = 0.0;
            //    for (int i = 0; i < data.Length; i++)
            //    {
            //        var output = FeedForward(data[i].Item1);
            //        cost += (CrossEntrophyFnc(data[i].Item1, data[i].Item2)/data.Length);
            //    }
            //    cost += 0.5 * (lambda/data.Length) * 
            //}

            #endregion

            #region Miscellaneous

            private static Random r = new Random();
            private void ShuffleList<T>(T[] list, Cancel cancelFnc, int numOfTranspositions = -1)
            {
                if (numOfTranspositions == -1)
                    numOfTranspositions = list.Length;
                for (int i = 0; i < numOfTranspositions; i++)
                {
                    if (cancelFnc != null && cancelFnc())
                        return;
                    int index1 = r.Next(0, list.Length);
                    int index2 = r.Next(0, list.Length);
                    var temp = list[index1];
                    list[index1] = list[index2];
                    list[index2] = temp;
                }
            }

            /// <summary>
            /// Generates a double within the Gaussian distribution
            /// From: http://stackoverflow.com/questions/218060/random-gaussian-variables
            ///     the Box-Muller Transformation http://mathworld.wolfram.com/Box-MullerTransformation.html
            /// </summary>
            /// <param name="mean"></param>
            /// <param name="stdDeviation"></param>
            /// <returns></returns>
            private double NextGaussianDistribution(double mean = 0, double stdDeviation = 1.0)
            {
                double u1 = r.NextDouble(); //these are uniform(0,1) random doubles
                double u2 = r.NextDouble();
                double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                             Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
                return mean + stdDeviation * randStdNormal; //random normal(mean,stdDev^2)
            }

            /// <summary>
            /// Returns the index of max value in the vector
            /// </summary>
            /// <param name="vec"></param>
            /// <returns></returns>
            public static int MaxArg(MyVector vec)
            {
                int maxArg = -1;
                double maxVal = double.MinValue;
                for (int i = 0; i < vec.Length; i++)
                {
                    if (vec[i] > maxVal)
                    {
                        maxArg = i;
                        maxVal = vec[i];
                    }
                }
                return maxArg;
            }

            #endregion
        }

        public class My2DMatrix : IXmlSerializable
        {
            private double[,] m_values;


            private My2DMatrix() { }
            public My2DMatrix(int rowsCount, int columnsCount)
            {
                m_values = new double[rowsCount, columnsCount];
            }

            /// <summary>
            /// Matrix created from two vectors v1 * v2^T
            /// </summary>
            /// <param name="v1"></param>
            /// <param name="v2"></param>
            public My2DMatrix(MyVector v1, MyVector v2)
            {
                m_values = new double[v1.Length, v2.Length];
                for (int i = 0; i < v1.Length; i++)
                {
                    for (int j = 0; j < v2.Length; j++)
                    {
                        m_values[i, j] = v1[i] * v2[j];
                    }
                }
            }

            public double this[int r, int c]
            {
                get { return m_values[r, c]; }
                set { m_values[r, c] = value; }
            }

            public int RowsCount { get { return m_values.GetLength(0); } }
            public int ColumnsCount { get { return m_values.GetLength(1); } }

            public int GetLength(int dimension)
            {
                return m_values.GetLength(dimension);
            }

            /// <summary>
            /// Each value multiplied by the constant
            /// </summary>
            /// <param name="constant"></param>
            /// <param name="matrix"></param>
            /// <returns></returns>
            public static My2DMatrix operator *(double constant, My2DMatrix matrix)
            {
                My2DMatrix result = new My2DMatrix(matrix.RowsCount, matrix.ColumnsCount);
                for (int i = 0; i < matrix.RowsCount; i++)
                {
                    for (int j = 0; j < matrix.ColumnsCount; j++)
                    {
                        result[i, j] = constant * matrix[i, j];
                    }
                }
                return result;
            }

            /// <summary>
            /// Sum of two matrices
            /// </summary>
            /// <param name="mat1"></param>
            /// <param name="mat2"></param>
            /// <returns></returns>
            public static My2DMatrix operator +(My2DMatrix mat1, My2DMatrix mat2)
            {
                if (mat1.RowsCount != mat2.RowsCount || mat1.ColumnsCount != mat2.ColumnsCount)
                {
                    throw new RankException("Wrong dimensions for sum operation.");
                }
                My2DMatrix result = new My2DMatrix(mat1.RowsCount, mat1.ColumnsCount);
                for (int i = 0; i < mat1.RowsCount; i++)
                {
                    for (int j = 0; j < mat1.ColumnsCount; j++)
                    {
                        result[i, j] = mat1[i, j] + mat2[i, j];
                    }
                }
                return result;
            }

            /// <summary>
            /// Difference of two matrices
            /// </summary>
            /// <param name="mat1"></param>
            /// <param name="mat2"></param>
            /// <returns></returns>
            public static My2DMatrix operator -(My2DMatrix mat1, My2DMatrix mat2)
            {
                if (mat1.RowsCount != mat2.RowsCount || mat1.ColumnsCount != mat2.ColumnsCount)
                {
                    throw new RankException("Wrong dimensions for subtract operation.");
                }
                My2DMatrix result = new My2DMatrix(mat1.RowsCount, mat1.ColumnsCount);
                for (int i = 0; i < mat1.RowsCount; i++)
                {
                    for (int j = 0; j < mat1.ColumnsCount; j++)
                    {
                        result[i, j] = mat1[i, j] - mat2[i, j];
                    }
                }
                return result;
            }

            public My2DMatrix Transpose()
            {
                My2DMatrix result = new My2DMatrix(ColumnsCount, RowsCount);
                for (int i = 0; i < RowsCount; i++)
                {
                    for (int j = 0; j < ColumnsCount; j++)
                    {
                        result[j, i] = this[i, j];
                    }
                }
                return result;
            }

#region Serialization
            public System.Xml.Schema.XmlSchema GetSchema()
            {
                return null;
            }

            public void ReadXml(System.Xml.XmlReader reader)
            {
                var xmlSerializer = new XmlSerializer(typeof(double[][]));
                reader.ReadToFollowing("ArrayOfArrayOfDouble");
                var temp = (double[][])xmlSerializer.Deserialize(reader);
                m_values = FromArrayOfArrays(temp);
            }

            public void WriteXml(System.Xml.XmlWriter writer)
            {
                var xmlSerializer = new XmlSerializer(typeof(double[][]));
                xmlSerializer.Serialize(writer, ToArrayOfArrays(this));
            }

            private static double[][] ToArrayOfArrays(My2DMatrix matrix)
            {
                double[][] result = new double[matrix.m_values.GetLength(0)][];
                for (int i = 0; i < matrix.m_values.GetLength(0); i++)
                {
                    result[i] = new double[matrix.m_values.GetLength(1)];
                    for (int j = 0; j < matrix.m_values.GetLength(1); j++)
                    {
                        result[i][j] = matrix.m_values[i, j];
                    }
                }
                return result;
            }

            private static double[,] FromArrayOfArrays(double[][] arrays)
            {
                double[,] result = new double[arrays.Length, arrays[0].Length];
                for (int i = 0; i < result.GetLength(0); i++)
                {
                    for (int j = 0; j < result.GetLength(1); j++)
                    {
                        result[i, j] = arrays[i][j];
                    }
                }
                return result;
            }
#endregion

            public override string ToString()
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("[");
                for (int i = 0; i < RowsCount; i++)
                {
                    if (i != 0)
                        sb.Append(" ");
                    for (int y = 0; y < ColumnsCount; y++)
                    {
                        sb.Append(m_values[i, y]);
                        sb.Append(", ");
                    }
                    sb.AppendLine();
                }
                sb.Remove(sb.Length - 4, 4);
                sb.Append("]");
                return sb.ToString();
            }
        }

        public class MyVector : IXmlSerializable
        {
            private double[] m_values;

            private MyVector() { }

            public MyVector(int size)
            {
                m_values = new double[size];
            }
            public MyVector(double[] values)
            {
                m_values = new double[values.Length];
                Array.Copy(values, m_values, values.Length);
            }
            public MyVector(MyVector vec)
            {
                m_values = new double[vec.Length];
                Array.Copy((Array)vec.m_values, (Array)m_values, (int)vec.Length);
            }
            public double this[int index]
            {
                get { return m_values[index]; }
                set { m_values[index] = value; }
            }

            public int Length { get { return m_values.Length; } }

            public static MyVector operator +(MyVector a1, MyVector a2)
            {
                if (a1.Length != a2.Length)
                {
                    throw new RankException("Different dimensions for sum operation.");
                }
                MyVector result = new MyVector(a1.Length);
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = a1[i] + a2[i];
                }
                return result;
            }

            public static MyVector operator -(MyVector a1, MyVector a2)
            {
                if (a1.Length != a2.Length)
                {
                    throw new RankException("Different dimensions for subtraction operation.");
                }
                MyVector result = new MyVector(a1.Length);
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = a1[i] - a2[i];
                }
                return result;
            }

            /// <summary>
            /// Implementation of dot product of matrix and a vector
            /// </summary>
            /// <param name="matrix"></param>
            /// <param name="myVector"></param>
            /// <returns></returns>
            public static MyVector operator *(My2DMatrix matrix, MyVector myVector)
            {
                if (matrix.GetLength(1) != myVector.Length)
                    throw new RankException("Wrong dimensions!");
                MyVector result = new MyVector(matrix.GetLength(0));
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = 0;
                    for (int j = 0; j < myVector.Length; j++)
                    {
                        result[i] += matrix[i, j] * myVector[j];
                    }
                }
                return result;
            }

            /// <summary>
            /// Implemetation of Hadamard product
            /// </summary>
            /// <param name="a1"></param>
            /// <param name="a2"></param>
            /// <returns></returns>
            public static MyVector operator *(MyVector a1, MyVector a2)
            {
                if (a1.Length != a2.Length)
                {
                    throw new RankException("Different dimensions for subtraction operation.");
                }
                MyVector result = new MyVector(a1.Length);
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = a1[i] * a2[i];
                }
                return result;
            }

            public static MyVector operator *(double constant, MyVector vec)
            {
                MyVector result = new MyVector(vec.Length);
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = constant * vec[i];
                }
                return result;
            }

            public delegate double Fnc(double value);
            public MyVector ApplyFunction(Fnc fnc)
            {
                MyVector result = new MyVector(Length);
                for (int i = 0; i < Length; i++)
                {
                    result[i] = fnc(m_values[i]);
                }
                return result;
            }

            public static MyVector UnitVector(int size, int index)
            {
                MyVector result = new MyVector(size);
                result[index] = 1;
                return result;
            }

#region Serialization
            public System.Xml.Schema.XmlSchema GetSchema()
            {
                return null;
            }

            public void ReadXml(System.Xml.XmlReader reader)
            {
                var xmlSerializer = new XmlSerializer(typeof(double[]));
                reader.Read();
                m_values = (double[])xmlSerializer.Deserialize(reader);
            }

            public void WriteXml(System.Xml.XmlWriter writer)
            {
                var xmlSerializer = new XmlSerializer(typeof(double[]));
                xmlSerializer.Serialize(writer, m_values);
            }
#endregion

            public override string ToString()
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("[");
                for (int i = 0; i < this.Length; i++)
                {
                    sb.Append(m_values[i]);
                    sb.Append(", ");
                }
                sb.Remove(sb.Length - 2, 2);
                return sb.ToString();
            }
        }
    }
}