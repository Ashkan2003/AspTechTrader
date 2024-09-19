import {
  LineChart,
  Line,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  ResponsiveContainer,
} from "recharts";


const SymbolChart = () => {

    const chartData = [
        {
            name: 'Page A',
            uv: 4000,
            pv: 2400,
            amt: 2400,
        },
        {
            name: 'Page B',
            uv: 3000,
            pv: 1398,
            amt: 2210,
        },
        {
            name: 'Page C',
            uv: 2000,
            pv: 9800,
            amt: 2290,
        },
        {
            name: 'Page D',
            uv: 2780,
            pv: 3908,
            amt: 2000,
        },
        {
            name: 'Page E',
            uv: 1890,
            pv: 4800,
            amt: 2181,
        },
        {
            name: 'Page F',
            uv: 2390,
            pv: 3800,
            amt: 2500,
        },
        {
            name: 'Page G',
            uv: 3490,
            pv: 4300,
            amt: 2100,
        },
    ];

  return (
      <div className="h-80">
          <ResponsiveContainer width="110%" height="100%">
        <LineChart
          width={500}
                  height={300}
                  data={chartData}
          margin={{
            top: 5,
            right: 25,
            left: 0,
            bottom: 5,
          }}
        >
          <CartesianGrid  strokeDasharray="3 2" />
          <XAxis  dataKey="name" />
          <YAxis />
          <Tooltip  animationEasing="ease-out"/>
          {/* <Legend /> */}
           <Line type="monotone" dataKey="pv" stroke="#8884d8" activeDot={{ r: 8 }} />
           <Line type="monotone" dataKey="uv" stroke="#5cd91d" />
        </LineChart>
      </ResponsiveContainer>
    </div>
  );
};

export default SymbolChart;
